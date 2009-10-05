using System;

namespace FluentWebControls
{
	public class ControllerInfo
	{
		public ControllerInfo(object aspxPage)
		{
			if (aspxPage != null)
			{
				Type type = aspxPage.GetType().BaseType;
				string[] parts = type.FullName.Split('.');
				Action = parts[parts.Length - 1];
				Name = parts[parts.Length - 2];
			}
		}

		public string Action { get; private set; }
		public string Name { get; private set; }
	}
}