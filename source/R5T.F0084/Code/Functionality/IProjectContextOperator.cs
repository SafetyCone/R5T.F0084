using System;

using R5T.T0132;
using R5T.T0153;


namespace R5T.F0084
{
	[FunctionalityMarker]
	public partial interface IProjectContextOperator : IFunctionalityMarker
	{
		public ProjectContext GetProjectContext(
			string projectFilePath,
			string projectDescription)
		{
			var projectDirectoryPath = F0052.ProjectPathsOperator.Instance.GetProjectDirectoryPath(projectFilePath);

			var projectName = F0052.ProjectPathsOperator.Instance.GetProjectName(projectFilePath);
			var projectDefaultNamespaceName = F0040.F000.ProjectNamespacesOperator.Instance.GetDefaultNamespaceName_FromProjectName(projectName);

			var output = new ProjectContext
			{
				ProjectFilePath = projectFilePath,
				ProjectDirectoryPath = projectDirectoryPath,
				ProjectName = projectName,
				ProjectDescription = projectDescription,
				ProjectDefaultNamespaceName = projectDefaultNamespaceName,
			};

			return output;
		}
	}
}