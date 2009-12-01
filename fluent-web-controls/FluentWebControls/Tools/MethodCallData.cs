using System.Collections.Generic;

namespace FluentWebControls.Tools
{
	public class MethodCallData
	{
		public string ClassName { get; set; }
		public string MethodName { get; set; }
		public Dictionary<string, string> ParameterValues { get; set; }
	}
}