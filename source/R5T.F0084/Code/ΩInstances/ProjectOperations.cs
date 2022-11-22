using System;


namespace R5T.F0084
{
	public class ProjectOperations : IProjectOperations
	{
		#region Infrastructure

	    public static IProjectOperations Instance { get; } = new ProjectOperations();

	    private ProjectOperations()
	    {
        }

	    #endregion
	}
}