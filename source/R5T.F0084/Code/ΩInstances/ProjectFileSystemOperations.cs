using System;


namespace R5T.F0084
{
	public class ProjectFileSystemOperations : IProjectFileSystemOperations
	{
		#region Infrastructure

	    public static IProjectFileSystemOperations Instance { get; } = new ProjectFileSystemOperations();

	    private ProjectFileSystemOperations()
	    {
        }

	    #endregion
	}
}