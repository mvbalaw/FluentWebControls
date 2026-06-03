@ECHO OFF
REM Manually publish the locally-built FluentWebControls package to the shared
REM NuGet feed (the "Mvba Nuget Feed" source consuming projects restore from).
REM Run Build.bat first so dist\ contains a fresh .nupkg.
copy "%~dp0dist\FluentWebControls.*.nupkg" "\\fs-dev\Build\MvbaNugetFeed"
@pause
