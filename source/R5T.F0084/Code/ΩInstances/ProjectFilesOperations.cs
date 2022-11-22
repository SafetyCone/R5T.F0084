using System;


namespace R5T.F0084
{
	public class ProjectFilesOperations : IProjectFilesOperations
	{
		#region Infrastructure

	    public static IProjectFilesOperations Instance { get; } = new ProjectFilesOperations();

	    private ProjectFilesOperations()
	    {
        }

	    #endregion
	}
}