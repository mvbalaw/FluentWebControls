FluentWebControls ReadMe
===
### Description

FluentWebControls is s a fluent interface for emitting web controls in C#.

### Sample Usages

### TextBox
	<%= Fluent.TextBoxFor(() => ViewData.Model.FirstName)
		.WithLabel("First Name:")
		.Width("200px") %>

### TextArea
	<%= Fluent.TextAreaFor(() => ViewData.Model.Comment)
		.WithLabel("Comments:")
		.Rows(5)
		.Width("400px") %>

### Button
	<%= Fluent.ButtonFor(ButtonData.ButtonType.New, this)
		.WithText("Add New " + ViewData.Model.BusinessObjectDescription)
		.VisibleIf(ViewData.Model.UserIsAdmin) %>

### CheckBox
	<%= Fluent.CheckBoxFor(() => ViewData.Model.Active)
		.WithLabel("Active")
		.IsChecked(ViewData.Model.IsActive) %>

### DropDownList
	<%= Fluent.DropDownListFor(Code.BoundPropertyNames.Type, ViewData.Model.DistinctCodeTypes, n => n.Name1, n => n.Name1)
		.WithDefault("Select", "")		    
		.WithSelectedValue(() => ViewData.Model.Type)
		.WithLabel("Code type:") %>

### ComboSelect
	<%= Fluent.ComboSelectFor(County.BoundPropertyNames.CourtIds, ViewData.Model.Courts, n => n.DisplayName, n => n.Id)
		.WithSelectedValues(ViewData.Model.SelectedCourts, court => court.CourtId)
		.WithLabel("Courts:") %>

### Link
	<%= Fluent.LinkTo(ControllerName, ActionName)
		.WithLinkText(">>")
		.WithMouseOverText("Last Page")
		.CssClass("linkHighlight")
		.DisabledIf(PagedListParameters.PageNumber == LastPage)
		.WithData(() => PagedListParameters.PageSize) %>

### Hidden input
	<%= Fluent.HiddenFor(() => PagedListParameters.PageSize) %>

	
### Input Builders
[See how][inputbuilders] we use these controls with our input builders

### How To Build:

The build script requires PowerShell 5.1 and MSBuild from Visual Studio or the Visual Studio Build Tools.

1. Open a command prompt to the root folder.
1. Run `Build.bat` to build, test, and create the NuGet package.
1. Run `Build.bat -Configuration Release` to build a Release package.
1. Run `Build.bat -Task test` to build and run tests without creating a package.
1. Run `Build.bat -Artifacts "\\server\share\FluentWebControls"` to write packages to a custom artifact folder.

### License

[MIT License][mitlicense]

This project is part of [MVBA's Open Source Projects][MvbaLawGithub].

If you have questions or comments about this project, please contact us at <mailto:opensource@mvbalaw.com>.
[inputbuilders]: http://shashankshetty.wordpress.com/2010/03/05/separation-of-concerns-in-input-builders/
[MvbaLawGithub]: http://mvbalaw.github.io/
[mitlicense]: http://www.opensource.org/licenses/mit-license.php

