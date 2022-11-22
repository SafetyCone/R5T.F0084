using System;


namespace R5T.F0084
{
	public class ProjectFilesOperator : IProjectFilesOperator
	{
		#region Infrastructure

	    public static IProjectFilesOperator Instance { get; } = new ProjectFilesOperator();

	    private ProjectFilesOperator()
	    {
        }

	    #endregion
	}
}