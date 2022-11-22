using System;


namespace R5T.F0084
{
	public class ProjectFileSystemOperator : IProjectFileSystemOperator
	{
		#region Infrastructure

	    public static IProjectFileSystemOperator Instance { get; } = new ProjectFileSystemOperator();

	    private ProjectFileSystemOperator()
	    {
        }

	    #endregion
	}
}