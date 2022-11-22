using System;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.F0084
{
	[FunctionalityMarker]
	public partial interface IProjectOperations : IFunctionalityMarker
	{
		public async Task CreateNewProject_WebApplicationServer(
			string projectFilePath,
			string projectDescription)
		{
			await ProjectOperator.Instance.CreateNewProject(
				projectFilePath,
				projectDescription,
				F0081.ProjectFileOperations.Instance.CreateNewProjectFile_Web,
				ProjectFileSystemOperations.Instance.SetupProjectFileSystem_Library);
		}

		public async Task CreateNewProject_Library(
			string projectFilePath,
			string projectDescription)
		{
			await ProjectOperator.Instance.CreateNewProject(
				projectFilePath,
				projectDescription,
				F0081.ProjectFileOperations.Instance.CreateNewProjectFile_Library,
				ProjectFileSystemOperations.Instance.SetupProjectFileSystem_Library);
		}

        public async Task CreateNewProject_Console(
			string projectFilePath,
			string projectDescription)
		{
			await ProjectOperator.Instance.CreateNewProject(
				projectFilePath,
				projectDescription,
				F0081.ProjectFileOperations.Instance.CreateNewProjectFile_Console,
				ProjectFileSystemOperations.Instance.SetupProjectFileSystem_Console);
		}

		public async Task CreateNewProject_WebServerForBlazorClient(
			string projectFilePath,
			string projectDescription)
		{
			await ProjectOperator.Instance.CreateNewProject(
				projectFilePath,
				projectDescription,
				F0081.ProjectFileOperations.Instance.CreateNewProjectFile_WebServerForBlazorClient,
				ProjectFileSystemOperations.Instance.SetupProjectFileSystem_WebServerForBlazorClient);
		}

        public async Task CreateNewProject_WebServerForBlazorClient(
            string projectFilePath,
            string projectDescription)
        {
            await ProjectOperator.Instance.CreateNewProject(
                projectFilePath,
                projectDescription,
                F0081.ProjectFileOperations.Instance.CreateNewProjectFile_WebServerForBlazorClient,
                ProjectFileSystemOperations.Instance.SetupProjectFileSystem_WebServerForBlazorClient);
        }
    }
}