using System;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class CommandItem
	{
		public static CommandItem<T> For<T>(Func<T, string> getHref)
		{
			return new CommandItem<T>(getHref);
		}
	}

	public interface ICommandItem
	{
		AlignAttribute Align { get; }
		string Alt { get; }
		//string Href { get; }
		string CssClass { get; }
		string ImageUrl { get; }
		string Text { get; }
	}

	public class CommandItem<T> : ICommandItem, IListItem<T>
	{
		private readonly Func<T, string> _getLink;

		public CommandItem(Func<T, string> getLink)
		{
			_getLink = getLink;
			Align = AlignAttribute.Left;
		}

		internal AlignAttribute Align { private get; set; }
		internal string Alt { private get; set; }
		internal string CssClass { private get; set; }
		internal string ImageUrl { private get; set; }
		internal string Text { private get; set; }

		string ICommandItem.Text
		{
			get { return Text; }
		}

		string ICommandItem.ImageUrl
		{
			get { return ImageUrl; }
		}

		string ICommandItem.Alt
		{
			get { return Alt; }
		}

		AlignAttribute ICommandItem.Align
		{
			get { return Align; }
		}

		string ICommandItem.CssClass
		{
			get { return CssClass; }
		}

		public StringBuilder Render(T item)
		{
			var listItem = new StringBuilder();
			listItem.Append("<");
			listItem.Append("div");
			listItem.Append(Align.Text.CreateQuotedAttribute("align"));
			listItem.Append(">");
			string link = getLink(item).ToString();
			listItem.Append(link);
			listItem.Append("</div>");
			return listItem;
		}

		private LinkData getLink(T item)
		{
			string navigateUrl = _getLink(item);
			string linkId = String.Format("lnk{0}", navigateUrl.Replace('/', '_').TrimStart(new[] { '_' }));
			return new LinkData().WithId(linkId).WithUrl(navigateUrl).WithLinkText(Text).WithLinkImageUrl(ImageUrl, Alt);
		}
	}
}