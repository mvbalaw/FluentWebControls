#Requires -Version 5.1
<#
.SYNOPSIS
    Builds the FluentWebControls solution.

.PARAMETER Configuration
    Build configuration: Debug or Release. Defaults to Debug.

.PARAMETER Artifacts
    Destination folder for the NuGet package. Overrides everything else.
    When omitted, the folder is resolved from -Environment (settings\<env>.yaml),
    and falls back to .\dist.

.PARAMETER Environment
    Build-server environment name. When supplied, the artifacts folder is read
    from settings\<Environment>.default.yaml then settings\<Environment>.yaml
    (the ':artifacts:' key), and the package is also published to the shared
    NuGet feed unless -Feed none is passed. Mirrors the old Rake 'environment=' arg.

.PARAMETER Feed
    Shared NuGet feed to publish the packed .nupkg to. Defaults to the MVBA feed
    when -Environment is set, and to none for plain local builds. Pass 'none' to
    skip publishing even when -Environment is set.

.PARAMETER Task
    Which task to run: default, clean, assemblyinfo, build, copy_artifacts, test,
    pack, publish, resolve. Defaults to 'default' (runs the full pipeline).

.PARAMETER TargetFramework
    Target framework output folder to stage for tests. Defaults to net48.

.EXAMPLE
    .\build.ps1
    .\build.ps1 -Configuration Release
    .\build.ps1 -Task test
    .\build.ps1 -Task resolve environment=devAgentMaster   # dry-run: show resolved paths
    .\Build.bat environment=devAgentMaster                 # build server (legacy arg form)
    .\build.ps1 -Artifacts "\\fsa\Shares2\dev\Build Artifacts\FluentWebControls\master"
#>
param(
    [string]$Configuration = "Debug",
    [string]$Artifacts     = "",
    [string]$Task          = "default",
    [string]$TargetFramework = "net48",
    [string]$Environment   = "",
    [string]$Feed          = "",
    [Parameter(ValueFromRemainingArguments = $true)]
    [string[]]$Extra
)

$ErrorActionPreference = "Stop"
$startTime = Get-Date

# --- Constants ---
$PRODUCT_NAME         = "FluentWebControls"
$CLR_TOOLS_VERSION    = "v4.8"
$DEFAULT_BUILD_NUMBER = "1.1.0"
$COMPANY_NAME         = "MVBA, P.C."
$COPYRIGHT            = "MVBA, P.C. (c) 2009-2020"
$ROOT                 = $PSScriptRoot
$STAGE                = Join-Path $ROOT "build"
$SETTINGS_DIR         = Join-Path $ROOT "settings"
# The shared folder-based NuGet feed that consuming projects restore from
# (declared as the "Mvba Nuget Feed" package source in NuGet.Config).
$DEFAULT_FEED         = "\\fs-dev\Build\MvbaNugetFeed"

$nunit_cmd = Join-Path $ROOT "tools\NUnit\nunit-console.exe"

# ---------------------------------------------------------------------------
# Legacy 'key=value' argument support (e.g. Build.bat environment=devAgentMaster).
# A bare 'key=value' token has no leading dash, so PowerShell binds it positionally
# to the first parameter ($Configuration). Pull those assignments back out here so
# the build server's existing Rake-style invocation keeps working unchanged.
# ---------------------------------------------------------------------------
$assignments = @{}
$positional  = @()
foreach ($tok in (@($Configuration) + @($Extra))) {
    if ([string]::IsNullOrWhiteSpace($tok)) { continue }
    if ($tok -match '^\s*([A-Za-z_]+)\s*=\s*(.*)$') {
        $assignments[$matches[1].ToLower()] = $matches[2].Trim()
    } else {
        $positional += $tok
    }
}

# Reset $Configuration if it actually captured a 'key=value' token, then re-apply
# a genuine positional configuration (e.g. Build.bat Release) and any assignments.
if ($Configuration -match '=') { $Configuration = "Debug" }
if ($positional.Count -ge 1 -and -not [string]::IsNullOrWhiteSpace($positional[0])) {
    $Configuration = $positional[0]
}
if ($assignments.ContainsKey('configuration')) { $Configuration = $assignments['configuration'] }
if ($assignments.ContainsKey('task'))          { $Task          = $assignments['task'] }
if ($assignments.ContainsKey('environment') -and $Environment -eq "") { $Environment = $assignments['environment'] }
if ($assignments.ContainsKey('artifacts')   -and $Artifacts   -eq "") { $Artifacts   = $assignments['artifacts'] }
if ($assignments.ContainsKey('feed')        -and $Feed        -eq "") { $Feed        = $assignments['feed'] }

$COMPILE_TARGET   = $Configuration
$TARGET_FRAMEWORK = $TargetFramework

# ---------------------------------------------------------------------------
# Helper functions
# ---------------------------------------------------------------------------

function Get-BuildNumber {
    try {
        $gittag = (& git describe --long 2>$null) -join ""
        if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($gittag)) {
            return $DEFAULT_BUILD_NUMBER
        }

        $gittag = $gittag.Trim()
        Write-Host "gittag: $gittag"
        $parts = $gittag -split "-"
        if ($parts.Count -lt 3) {
            return $DEFAULT_BUILD_NUMBER
        }

        $base_version = $parts[0] -replace "^v", ""
        $git_build_revision = $parts[1]
        $git_short_hash = $parts[2]
        Write-Host "base_version:       $base_version"
        Write-Host "git_build_revision: $git_build_revision"
        Write-Host "git_short_hash:     $git_short_hash"
        return "$base_version.$git_build_revision"
    } catch {
        return $DEFAULT_BUILD_NUMBER
    }
}

function Get-GitHash {
    try {
        $gittag = (& git describe --long 2>$null) -join ""
        if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($gittag)) {
            return "git unavailable"
        }

        $parts = $gittag.Trim() -split "-"
        if ($parts.Count -lt 3) {
            return "git unavailable"
        }

        return $parts[2]
    } catch {
        return "git unavailable"
    }
}

function Resolve-ArtifactsFromSettings {
    # Reads the ':artifacts:' value from settings\<env>.default.yaml then
    # settings\<env>.yaml (later file wins). Returns $null when nothing is found.
    param(
        [string]$SettingsDir,
        [string]$Env
    )

    $artifacts = $null
    foreach ($name in @("$Env.default.yaml", "$Env.yaml")) {
        $file = Join-Path $SettingsDir $name
        if (Test-Path $file) {
            foreach ($line in (Get-Content $file)) {
                # matches ':artifacts: value' (Ruby symbol style) or 'artifacts: value'
                if ($line -match '^\s*:?artifacts:\s*(.+?)\s*$') {
                    $artifacts = $matches[1].Trim().Trim('"').Trim("'")
                }
            }
        }
    }

    if ($artifacts) {
        # Normalize forward slashes (the YAML uses //server/share form) to UNC backslashes.
        $artifacts = $artifacts -replace '/', '\'
    }
    return $artifacts
}

function Find-MSBuild {
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vswhere) {
        $found = & $vswhere -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" 2>$null |
            Select-Object -First 1

        if ($found -and (Test-Path $found)) {
            return $found
        }
    }

    $candidates = @(
        "${env:ProgramFiles}\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
        "${env:ProgramFiles}\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
        "${env:ProgramFiles}\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe"
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"
    )

    foreach ($candidate in $candidates) {
        if (Test-Path $candidate) {
            return $candidate
        }
    }

    $onPath = Get-Command msbuild.exe -ErrorAction SilentlyContinue
    if ($onPath) {
        return $onPath.Source
    }

    throw "MSBuild not found. Install Visual Studio or the Build Tools."
}

function Copy-OutputFiles {
    param(
        [string]$FromDir,
        [string[]]$Extensions,
        [string]$OutDir
    )

    if (-not (Test-Path $FromDir)) {
        Write-Warning "Source directory not found, skipping: $FromDir"
        return
    }

    Get-ChildItem -Path $FromDir |
        Where-Object { -not $_.PSIsContainer -and $_.Extension -in $Extensions } |
        ForEach-Object { Copy-Item $_.FullName -Destination $OutDir -Force }
}

# ---------------------------------------------------------------------------
# Resolve artifacts + feed destinations (after legacy args are normalized)
# ---------------------------------------------------------------------------

if ($Artifacts -ne "") {
    $ARTIFACTS = $Artifacts
} elseif ($Environment -ne "") {
    $resolved = Resolve-ArtifactsFromSettings -SettingsDir $SETTINGS_DIR -Env $Environment
    if ($resolved) {
        $ARTIFACTS = $resolved
    } else {
        Write-Warning "No ':artifacts:' found in settings for environment '$Environment'; using local dist."
        $ARTIFACTS = Join-Path $ROOT "dist"
    }
} else {
    $ARTIFACTS = Join-Path $ROOT "dist"
}

# Feed: explicit -Feed wins; otherwise publish to the shared feed only for an
# environment (build-server) build. 'none' disables publishing entirely.
if ($Feed -eq "none") {
    $FEED = ""
} elseif ($Feed -ne "") {
    $FEED = $Feed
} elseif ($Environment -ne "") {
    $FEED = $DEFAULT_FEED
} else {
    $FEED = ""
}

$script:BuildNumber = Get-BuildNumber

Write-Host ""
Write-Host "=== configuration ==="
Write-Host "Task:          $Task"
Write-Host "Configuration: $COMPILE_TARGET"
Write-Host "Environment:   $(if ($Environment -ne '') { $Environment } else { '(local)' })"
Write-Host "Build number:  $script:BuildNumber"
Write-Host "Artifacts:     $ARTIFACTS"
Write-Host "Feed:          $(if ($FEED -ne '') { $FEED } else { '(none - not published)' })"

# ---------------------------------------------------------------------------
# Tasks
# ---------------------------------------------------------------------------

function Invoke-Clean {
    Write-Host ""
    Write-Host "=== clean ==="
    Write-Host "Preparing build directory: $STAGE"
    if (Test-Path $STAGE) {
        Remove-Item -Recurse -Force $STAGE
    }

    New-Item -ItemType Directory -Path $STAGE | Out-Null

    if (-not (Test-Path $ARTIFACTS)) {
        New-Item -ItemType Directory -Path $ARTIFACTS | Out-Null
    }
}

function Invoke-AssemblyInfo {
    Write-Host ""
    Write-Host "=== assemblyinfo ==="
    $build_number = $script:BuildNumber
    $git_hash = Get-GitHash
    $outFile = Join-Path $ROOT "src\Directory.Build.props"

    Write-Host "Writing $outFile  (version $build_number, hash $git_hash)"

    $content = @"
<Project>
  <PropertyGroup>
    <Authors>$COMPANY_NAME</Authors>
    <Company>$COMPANY_NAME</Company>
    <Description>A fluent interface for emitting web controls in C#. (git sha for this version: $git_hash)</Description>
    <Copyright>$COPYRIGHT</Copyright>
    <PackageLicenseFile>License.txt</PackageLicenseFile>
    <NeutralLanguage>en-US</NeutralLanguage>
    <RepositoryUrl>https://github.com/mvbalaw/$PRODUCT_NAME</RepositoryUrl>
    <RepositoryType>git</RepositoryType>
    <Version>$build_number</Version>
    <FileVersion>$build_number</FileVersion>
    <ProductName>$PRODUCT_NAME</ProductName>
  </PropertyGroup>

  <ItemGroup>
    <Content Include="..\..\License.txt" Pack="true" Visible="false" PackagePath="" />
  </ItemGroup>
</Project>
"@

    [System.IO.File]::WriteAllText($outFile, $content, [System.Text.UTF8Encoding]::new($false))
}

function Invoke-Build {
    Write-Host ""
    Write-Host "=== build ==="
    Write-Host "Compiling $PRODUCT_NAME in $COMPILE_TARGET mode..."
    $msbuild = Find-MSBuild
    Write-Host "Using MSBuild: $msbuild"
    & $msbuild "src\$PRODUCT_NAME.sln" "/t:Restore;Rebuild" /verbosity:quiet /nologo /p:Configuration=$COMPILE_TARGET
    if ($LASTEXITCODE -ne 0) {
        throw "MSBuild failed (exit code $LASTEXITCODE)."
    }
}

function Invoke-CopyArtifacts {
    Write-Host ""
    Write-Host "=== copy_artifacts ==="
    Write-Host "Copying build outputs to: $STAGE"
    $extensions = @(".dll", ".pdb", ".exe")
    Copy-OutputFiles "src\$PRODUCT_NAME\bin\$COMPILE_TARGET\$TARGET_FRAMEWORK" $extensions $STAGE
    Copy-OutputFiles "src\$PRODUCT_NAME.Tests\bin\$COMPILE_TARGET\$TARGET_FRAMEWORK" $extensions $STAGE
}

function Invoke-Test {
    Write-Host ""
    Write-Host "=== test ==="
    if (-not (Test-Path $nunit_cmd)) {
        throw "NUnit runner not found at $nunit_cmd."
    }

    $testAssembly = Join-Path $STAGE "$PRODUCT_NAME.Tests.dll"
    Write-Host "Running unit tests: $testAssembly"
    & $nunit_cmd $testAssembly /framework $CLR_TOOLS_VERSION
    if ($LASTEXITCODE -ne 0) {
        throw "NUnit tests failed (exit code $LASTEXITCODE)."
    }
}

function Invoke-Pack {
    Write-Host ""
    Write-Host "=== pack ==="
    Write-Host "Creating NuGet package in: $ARTIFACTS"
    $msbuild = Find-MSBuild
    & $msbuild "src\$PRODUCT_NAME\$PRODUCT_NAME.csproj" /p:PackageOutputPath="$ARTIFACTS" /p:Configuration=$COMPILE_TARGET /t:pack /verbosity:quiet /nologo
    if ($LASTEXITCODE -ne 0) {
        throw "MSBuild pack failed (exit code $LASTEXITCODE)."
    }

    Write-Host "Artifacts available at: $ARTIFACTS"
}

function Invoke-PublishFeed {
    Write-Host ""
    Write-Host "=== publish ==="
    if ([string]::IsNullOrWhiteSpace($FEED)) {
        Write-Host "No feed configured; skipping feed publish. (Pass -Feed <path> or -Environment to publish.)"
        return
    }

    if (-not (Test-Path $FEED)) {
        throw "NuGet feed not reachable: $FEED"
    }

    $pkgName = "$PRODUCT_NAME.$script:BuildNumber.nupkg"
    $pkgPath = Join-Path $ARTIFACTS $pkgName
    if (-not (Test-Path $pkgPath)) {
        # Fall back to the newest matching package in the artifacts folder.
        $pkg = Get-ChildItem -Path $ARTIFACTS -Filter "$PRODUCT_NAME.*.nupkg" -ErrorAction SilentlyContinue |
            Sort-Object LastWriteTime -Descending | Select-Object -First 1
        if (-not $pkg) {
            throw "No $PRODUCT_NAME .nupkg found in $ARTIFACTS to publish."
        }
        $pkgPath = $pkg.FullName
        $pkgName = $pkg.Name
    }

    Write-Host "Publishing $pkgName to feed: $FEED"
    Copy-Item -Path $pkgPath -Destination $FEED -Force
    Write-Host "Published: $(Join-Path $FEED $pkgName)"
}

function Invoke-Resolve {
    # Diagnostics already printed above; this is a no-op dry-run target so the
    # resolved Task/Configuration/Environment/Artifacts/Feed can be confirmed
    # without building or touching the network.
    Write-Host ""
    Write-Host "=== resolve (dry run - nothing built or published) ==="
}

# ---------------------------------------------------------------------------
# Task dispatcher
# ---------------------------------------------------------------------------

switch ($Task.ToLower()) {
    "resolve" { Invoke-Resolve }
    "clean" { Invoke-Clean }
    "assemblyinfo" { Invoke-AssemblyInfo }
    "build" {
        Invoke-Clean
        Invoke-AssemblyInfo
        Invoke-Build
    }
    "copy_artifacts" {
        Invoke-Clean
        Invoke-AssemblyInfo
        Invoke-Build
        Invoke-CopyArtifacts
    }
    "test" {
        Invoke-Clean
        Invoke-AssemblyInfo
        Invoke-Build
        Invoke-CopyArtifacts
        Invoke-Test
    }
    "pack" {
        Invoke-Clean
        Invoke-AssemblyInfo
        Invoke-Build
        Invoke-CopyArtifacts
        Invoke-Test
        Invoke-Pack
    }
    "publish" {
        # Gate: pack the package and copy it to the feed ONLY after the build
        # succeeds and the tests pass. Invoke-Build / Invoke-Test throw on any
        # failure (ErrorActionPreference = Stop), aborting before Pack/PublishFeed.
        Invoke-Clean
        Invoke-AssemblyInfo
        Invoke-Build
        Invoke-CopyArtifacts
        Invoke-Test
        Invoke-Pack
        Invoke-PublishFeed
    }
    "default" {
        # Same gate as 'publish': nothing is packed or published unless the build
        # succeeds and all tests pass first.
        Invoke-Clean
        Invoke-AssemblyInfo
        Invoke-Build
        Invoke-CopyArtifacts
        Invoke-Test
        Invoke-Pack
        Invoke-PublishFeed
    }
    default {
        Write-Error "Unknown task '$Task'. Valid tasks: default, clean, assemblyinfo, build, copy_artifacts, test, pack, publish, resolve"
        exit 1
    }
}

$elapsed = (Get-Date) - $startTime
Write-Host ""
Write-Host "Build Succeeded - time elapsed: $([Math]::Round($elapsed.TotalSeconds, 1)) seconds"
