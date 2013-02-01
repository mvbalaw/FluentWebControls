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

		public static CommandItem<T> For<T>(Func<T, string, string> getControl)
		{
			return new CommandItem<T>(getControl);
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
		bool WrapWithSpan { get; }
	}

	public class CommandItem<T> : ICommandItem, IListItem<T>
	{
		private readonly Func<T, string, string> _getControl;
		private readonly Func<T, string> _getLink;

		public CommandItem(Func<T, string> getLink)
		{
			_getLink = getLink;
			Align = AlignAttribute.Left;
		}

		public CommandItem(Func<T, string, string> getControl)
		{
			_getControl = getControl;
			Align = AlignAttribute.Center;
		}

		internal AlignAttribute Align { private get; set; }
		internal string Alt { private get; set; }
		internal string CssClass { private get; set; }
		internal string ImageUrl { private get; set; }
		internal string Text { private get; set; }
		internal bool WrapWithSpan { private get; set; }

		#region ICommandItem Members

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

		bool ICommandItem.WrapWithSpan
		{
			get { return WrapWithSpan; }
		}

		#endregion

		#region IListItem<T> Members

		public StringBuilder Render(T item)
		{
			var listItem = new StringBuilder();
			string tag = WrapWithSpan ? "span" : "div";
			listItem.Append('<');
			listItem.Append(tag);
			listItem.Append(Align.Text.CreateQuotedAttribute("align"));
			if (CssClass != null)
			{
				listItem.Append(CssClass.CreateQuotedAttribute("class"));
			}
			listItem.Append('>');
			string control = _getControl == null ? GetLink(item).ToString() : _getControl(item, Text);
			listItem.Append(control);
			listItem.Append("</");
			listItem.Append(tag);
			listItem.Append('>');
			return listItem;
		}

		#endregion

		private LinkData GetLink(T item)
		{
			string navigateUrl = _getLink(item);
			string linkId = String.Format("lnk{0}", navigateUrl.Replace('/', '_').TrimStart(new[] {'_'}));
			return new LinkData().WithId(linkId).WithUrl(navigateUrl).WithLinkText(Text).WithLinkImageUrl(ImageUrl, Alt);
		}
	}
}