namespace FluentWebControls.Extensions
{
	public static class TExtensions
	{
		public static string CreateQuotedAttribute<T>(this T? value, string name) where T : struct
		{
			var v = value == null ? "" : value.ToString();
			return v.CreateQuotedAttribute(name);
		}

		public static string CreateQuotedAttribute<T>(this T value, string name) where T : struct
		{
			var v = value.ToString();
			return v.CreateQuotedAttribute(name);
		}

		public static string EscapeForTagAttribute<T>(this T? value) where T : struct
		{
			var v = value.ToString();
			return value == null ? "" : v.EscapeForTagAttribute();
		}

		public static string EscapeForTagAttribute<T>(this T value) where T : struct
		{
			var v = value.ToString();
			return v.EscapeForTagAttribute();
		}
	}
}