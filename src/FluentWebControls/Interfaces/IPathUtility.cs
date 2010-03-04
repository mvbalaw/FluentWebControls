namespace FluentWebControls.Interfaces
{
	public interface IPathUtility
	{
		/// <summary>
		/// example:
		/// input: "Users.mvc/Edit"
		/// output: "/myapp/Users.mvc/Edit"
		/// </summary>
		/// <param name="virtualDirectory"></param>
		/// <returns>external url to the given resource</returns>
		string GetUrl(string virtualDirectory);
	}
}