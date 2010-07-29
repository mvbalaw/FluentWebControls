namespace FluentWebControls.Extensions
{
	public static class CommandItemExtensions
	{
		public static CommandItem<T> AlignLeft<T>(this CommandItem<T> commandItem)
		{
			commandItem.Align = AlignAttribute.Left;
			return commandItem;
		}

		public static CommandItem<T> AlignRight<T>(this CommandItem<T> commandItem)
		{
			commandItem.Align = AlignAttribute.Right;
			return commandItem;
		}

		public static CommandItem<T> WithCssClass<T>(this CommandItem<T> commandItem, string cssClass)
		{
			commandItem.CssClass = cssClass;
			return commandItem;
		}

		public static CommandItem<T> WithText<T>(this CommandItem<T> commandItem, string text)
		{
			commandItem.Text = text;
			return commandItem;
		}

		public static CommandItem<T> WithImage<T>(this CommandItem<T> commandItem, string imageUrl)
		{
			return commandItem.WithImage(imageUrl, "");
		}
		
		public static CommandItem<T> WithImage<T>(this CommandItem<T> commandItem, string imageUrl, string alt)
		{
			commandItem.ImageUrl = imageUrl;
			commandItem.Alt = alt;
			return commandItem;
		}
	}
}