using System;
using System.Threading.Tasks;

using R5T.T0132;

using R5T.F0084.T002;


namespace R5T.F0084
{
	[FunctionalityMarker]
	public partial interface IProjectFileSystemOperations : IFunctionalityMarker
	{
		public async Task SetupProjectFileSystem_Console(
			ProjectContext projectContext)
		{
			await this.SetupProjectFileSystem_Initial(projectContext);

			await this.CreateProgramFile_Console(projectContext);
		}

		public async Task SetupProjectFileSystem_WebServerForBlazorClient(
			ProjectContext projectContext)
		{
			await this.SetupProjectFileSystem_Initial(projectContext);

			// Create the Properties directory.
			await this.CreatePropertiesDirectory(projectContext);

			// Launch settings JSON file.
			await this.CreateLaunchSettingsJsonFile_WebServerForBlazorClient(projectContext);

			// AppSettings JSON file.
			await this.CreateAppSettingsJsonFile(projectContext);

			// Development AppSettings JSON file.
			await this.CreateAppSettingsDevelopmentJsonFile(projectContext);

			// Program file for web server for Blazor client.
			await this.CreateProgramFile_WebServerForBlazorClient(projectContext);
		}

        public Task CreateAppSettingsDevelopmentJsonFile(ProjectContext projectContext)
        {
            var appsettingsJsonFilePath = Instances.ProjectPathsOperator.GetAppSettingsDevelopmentJsonFilePath(
                projectContext.ProjectFilePath);

            Instances.TextFileGenerator.CreateAppSettingsDevelopmentJsonFile(appsettingsJsonFilePath);

            return Task.CompletedTask;
        }

        public Task CreateAppSettingsJsonFile(ProjectContext projectContext)
		{
            var appsettingsJsonFilePath = Instances.ProjectPathsOperator.GetAppSettingsJsonFilePath(
				projectContext.ProjectFilePath);

            Instances.TextFileGenerator.CreateAppSettingsJsonFile(appsettingsJsonFilePath);

			return Task.CompletedTask;
        }

		public async Task CreateLaunchSettingsJsonFile_WebServerForBlazorClient(ProjectContext projectContext)
		{
			var launchSettingsJsonFilePath = Instances.ProjectPathsOperator.GetLaunchSettingsJsonFilePath(
				projectContext.ProjectFilePath);

			await Instances.CodeFileGenerationOperations.CreateLaunchSettings_WebServerForBlazorClient(
				launchSettingsJsonFilePath,
				projectContext.ProjectName);
		}

		public async Task SetupProjectFileSystem_Library(
			ProjectContext projectContext)
		{
			await this.SetupProjectFileSystem_Initial(projectContext);
		}

		public async Task SetupProjectFileSystem_Initial(
			ProjectContext projectContext)
		{
			// Create project plan Markdown file.
			await this.CreateProjectPlanMarkdownFile(projectContext);

			// Create code directory.
			await this.CreateCodeDirectory(projectContext);
			
			// Create Instances file.
			await this.CreateInstancesFile(projectContext);

			// Create documentation file.
			await this.CreateDocumentationFile(projectContext);
		}

		public Task CreatePropertiesDirectory(ProjectContext projectContext)
		{
			var propertiesDirectoryPath = Instances.ProjectPathsOperator.GetPropertiesDirectoryPath(
				projectContext.ProjectFilePath);

			Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(propertiesDirectoryPath);

			return Task.CompletedTask;
		}

		public Task CreateCodeDirectory(ProjectContext projectContext)
		{
			var codeDirectoryPath = F0052.ProjectPathsOperator.Instance.GetCodeDirectoryPath(
				projectContext.ProjectFilePath);

			F0000.FileSystemOperator.Instance.CreateDirectory_OkIfAlreadyExists(codeDirectoryPath);

			return Task.CompletedTask;
		}

		public async Task CreateDocumentationFile(ProjectContext projectContext)
		{
			var documentationCodeFilePath = F0052.ProjectPathsOperator.Instance.GetDocumentationFilePath(
				projectContext.ProjectFilePath);

			await F0083.CodeFileGenerationOperations.Instance.CreateDocumentationFile(
				documentationCodeFilePath,
				projectContext.ProjectDefaultNamespaceName,
				projectContext.ProjectDescription);
		}

		public Task CreateInstancesFile(ProjectContext projectContext)
		{
			var instancesCodeFilePath = F0052.ProjectPathsOperator.Instance.GetInstancesFilePath(
				projectContext.ProjectFilePath);

			F0053.CodeFileGenerator.Instance.CreateInstancesFile(
				instancesCodeFilePath,
				projectContext.ProjectDefaultNamespaceName);

			return Task.CompletedTask;
		}

		public async Task CreateProgramFile_Console(ProjectContext projectContext)
		{
			var programCodeFilePath = F0052.ProjectPathsOperator.Instance.GetProgramFilePath(
				projectContext.ProjectFilePath);

			await F0083.CodeFileGenerationOperations.Instance.CreateProgramFile_Console(
				programCodeFilePath,
				projectContext.ProjectDefaultNamespaceName);
		}

        public async Task CreateProgramFile_WebServerForBlazorClient(ProjectContext projectContext)
        {
            var programCodeFilePath = F0052.ProjectPathsOperator.Instance.GetProgramFilePath(
                projectContext.ProjectFilePath);

            await F0083.CodeFileGenerationOperations.Instance.CreateProgramFile_WebServerForBlazorClient(
                programCodeFilePath,
                projectContext.ProjectDefaultNamespaceName);
        }

        public Task CreateProjectPlanMarkdownFile(ProjectContext projectContext)
		{
			var projectPlanFilePath = F0052.ProjectPathsOperator.Instance.GetProjectPlanMarkdownFilePath(
				projectContext.ProjectFilePath);

			F0054.TextFileGenerator.Instance.CreateProjectPlanMarkdownFile(
				projectPlanFilePath,
				projectContext.ProjectName,
				projectContext.ProjectDescription);

			return Task.CompletedTask;
		}
	}
}