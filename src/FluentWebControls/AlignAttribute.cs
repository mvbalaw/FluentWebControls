//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.Web.UI.WebControls;

namespace FluentWebControls
{
	public class AlignAttribute
	{
		public static AlignAttribute Center = new AlignAttribute("center", HorizontalAlign.Center);
		public static AlignAttribute Left = new AlignAttribute("left", HorizontalAlign.Left);
		public static AlignAttribute Right = new AlignAttribute("right", HorizontalAlign.Right);
		private readonly HorizontalAlign _horizontalAlign;

		private AlignAttribute(string text, HorizontalAlign horizontalAlign)
		{
			_horizontalAlign = horizontalAlign;
			Text = text;
		}

		public string Text { get; }

		public HorizontalAlign ToHorizontalAlign()
		{
			return _horizontalAlign;
		}

		public override string ToString()
		{
			return $" align='{Text}'";
		}
	}
}