using System.IO;
using System.Text;
using System.Web.UI;

namespace FluentWebControls.Controls
{
	public interface IShouldHaveDifferentNameAndId
	{
	}

	public class CheckBoxWithDifferentNameAndId : System.Web.UI.WebControls.CheckBox, IShouldHaveDifferentNameAndId
	{
		private string _name;
		private string _id;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public override string ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public override string UniqueID
		{
			get { return _name ?? _id; }
		}

		protected override void Render(HtmlTextWriter writer)
		{
			var stream = new MemoryStream();
			using (var streamWriter = new StreamWriter(stream))
			{
				using (var localWrtier = new HtmlTextWriter(streamWriter))
				{

					base.Render(localWrtier);
				}
			}

			var text = Encoding.UTF8.GetString(stream.ToArray());
			// this is because the built-in rendered uses the same value for Name and Id
			// and we may want them to be different
			var updatedText = UniqueID == ID ? text : text.Replace("id=\"" + _name + "\"", "id=\"" + _id + "\"");
			writer.Write(updatedText);
		}
	}
}