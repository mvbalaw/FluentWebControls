This is a fluent interface for emitting web controls in c#.

## Sample Usages

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

	
## Input Builders
[See how][inputbuilders] we use these controls with our input builders

## License		

[MIT License][mitlicense]

This project is part of [MVBA Law Commons][mvbalawcommons].

[mvbalawcommons]: http://code.google.com/p/mvbalaw-commons/
[mitlicense]: http://www.opensource.org/licenses/mit-license.php
[inputbuilders]: http://shashankshetty.wordpress.com/2010/03/05/separation-of-concerns-in-input-builders/
