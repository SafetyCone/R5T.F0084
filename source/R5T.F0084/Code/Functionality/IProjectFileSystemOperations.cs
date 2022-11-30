using System;
using System.Threading.Tasks;

using R5T.T0132;

using R5T.F0084.T002;
using System.ComponentModel;

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

        public async Task SetupProjectFileSystem_WebBlazorClient(
            ProjectContext projectContext)
        {
            await this.SetupProjectFileSystem_Initial(projectContext);

            // Create the Properties directory.
            await this.CreatePropertiesDirectory(projectContext);

            // Launch settings JSON file.
            await this.CreateLaunchSettingsJsonFile_WebServerForBlazorClient(projectContext);

			// NPM package.json.
			await this.CreatePackageJsonFile(projectContext);

			// TailwindCSS config.
			await this.CreateTailwindConfigJsFile(projectContext);

			// source directory
			// source/css directory
			// Tailwind.css file.
			await this.CreateTailwindCssFile(projectContext);

			// wwwroot directory
			await this.CreateWwwRootDirectory(projectContext);

			// index.html file
			await this.CreateIndexHtmlFile(projectContext);

			// Pages directory
			await this.CreatePagesDirectory(projectContext);

            // Shared directory
            await this.CreateSharedDirectory(projectContext);

            // Components directory
            await this.CreateComponentsDirectory(projectContext);

			// Program file for web Blazor client.
			await this.CreateProgramFile_WebBlazorClient(projectContext);

			// App.razor file
			await this.CreateAppRazorFile_WebBlazorClient(projectContext);

			// _Imports.razor file
			await this.CreateImportsRazorFile_WebBlazorClient_Main(projectContext);

			// Shared/MainLayout.razor file
			await this.CreateMainLayoutRazorFile_WebBlazorClient(projectContext);

			// Pages/Index.razor file
			await this.CreateIndexRazorFile_WebBlazorClient(projectContext);
        }

        public async Task CreateIndexRazorFile_WebBlazorClient(ProjectContext projectContext)
        {
            var mainImportsRazorFilePath = Instances.ProjectPathsOperator.GetIndexRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateIndexRazorFile_WebBlazorClient(
                mainImportsRazorFilePath);
        }

        public async Task CreateMainLayoutRazorFile_WebBlazorClient(ProjectContext projectContext)
        {
            var mainImportsRazorFilePath = Instances.ProjectPathsOperator.GetMainLayoutRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateMainLayoutRazorFile_WebBlazorClient(
                mainImportsRazorFilePath);
        }

        public async Task CreateImportsRazorFile_WebBlazorClient_Main(ProjectContext projectContext)
        {
            var mainImportsRazorFilePath = Instances.ProjectPathsOperator.GetMainImportsRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateImportsRazorFile_WebBlazorClient_Main(
                mainImportsRazorFilePath);
        }

        public async Task CreateAppRazorFile_WebBlazorClient(ProjectContext projectContext)
        {
            var appRazorFilePath = Instances.ProjectPathsOperator.GetAppRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateAppRazorFile_WebBlazorClient(
                appRazorFilePath);
        }

        public Task CreatePagesDirectory(ProjectContext projectContext)
		{
			var pagesDirectoryPath = Instances.ProjectPathsOperator.GetPagesDirectoryPath(
				projectContext.ProjectFilePath);

			Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(pagesDirectoryPath);

			return Task.CompletedTask;
		}

        public Task CreateSharedDirectory(ProjectContext projectContext)
        {
            var sharedDirectoryPath = Instances.ProjectPathsOperator.GetSharedDirectoryPath(
                projectContext.ProjectFilePath);

            Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(sharedDirectoryPath);

            return Task.CompletedTask;
        }

        public Task CreateComponentsDirectory(ProjectContext projectContext)
        {
            var componentsDirectoryPath = Instances.ProjectPathsOperator.GetComponentsDirectoryPath(
                projectContext.ProjectFilePath);

            Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(componentsDirectoryPath);

            return Task.CompletedTask;
        }

        public async Task CreateIndexHtmlFile(ProjectContext projectContext)
		{
			var pageTitle = projectContext.ProjectName;

			var indexHtmlFilePath = Instances.ProjectPathsOperator.GetWwwRootIndexHtmlFilePath(
				projectContext.ProjectFilePath);

			await Instances.CodeFileGenerationOperations.CreateIndexHtmlFile(
				indexHtmlFilePath,
				pageTitle);
		}

		public Task CreateWwwRootDirectory(ProjectContext projectContext)
		{
			var wwwRootDirectoryPath = Instances.ProjectPathsOperator.GetWwwRootDirectoryPath(
				projectContext.ProjectFilePath);

			Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(wwwRootDirectoryPath);

			return Task.CompletedTask;
		}

		public async Task CreateTailwindCssFile(ProjectContext projectContext)
		{
			var tailwindCssFilePath = Instances.ProjectPathsOperator.GetTailwindCssFilePath(
				projectContext.ProjectFilePath);

			await Instances.CodeFileGenerationOperations.CreateTailwindCssFile(
				tailwindCssFilePath);
		}

		public async Task CreateTailwindConfigJsFile(ProjectContext projectContext)
		{
            var tailwindConfigJsFilePath = Instances.ProjectPathsOperator.GetTailwindConfigJsFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateTailwindConfigJsFile(
                tailwindConfigJsFilePath);
        }

		public async Task CreatePackageJsonFile(ProjectContext projectContext)
		{
            var packageJsonFilePath = Instances.ProjectPathsOperator.GetPackageJsonFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreatePackageJsonFile(
                packageJsonFilePath,
                projectContext.ProjectName,
				projectContext.ProjectDescription);
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

        public async Task CreateProgramFile_WebBlazorClient(ProjectContext projectContext)
        {
            var programCodeFilePath = F0052.ProjectPathsOperator.Instance.GetProgramFilePath(
                projectContext.ProjectFilePath);

            await F0083.CodeFileGenerationOperations.Instance.CreateProgramFile_WebBlazorClient(
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