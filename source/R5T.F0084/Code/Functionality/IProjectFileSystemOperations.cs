using System;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using R5T.F0078;
using R5T.F0089;
using R5T.L0026;
using R5T.T0132;
using R5T.T0153;


namespace R5T.F0084
{
	[FunctionalityMarker]
	public partial interface IProjectFileSystemOperations : IFunctionalityMarker
	{
        public async Task AddTailwindCssTypograpy(ProjectContext projectContext)
        {
            // Add @tailwindcss/typography, 0.5.4 to the package.json file.
            var packageJsonFilePath = Instances.ProjectPathsOperator.GetPackageJsonFilePath(projectContext.ProjectFilePath);

            var packageJson = await Instances.JsonOperator.Deserialize(packageJsonFilePath);

            var devDependencies = packageJson["devDependencies"] as JObject;

            devDependencies.Add("@tailwindcss/typography", "0.5.4");

            Instances.JsonOperator.Serialize_Synchronous(
                packageJsonFilePath,
                packageJson);
        }

        public async Task CreateExampleComponent(ProjectContext projectContext)
        {
            var exampleComponentRazorFilePath = Instances.ProjectPathsOperator.GetExampleComponentRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateExampleComponentRazorFile(
                exampleComponentRazorFilePath,
                projectContext.ProjectDefaultNamespaceName);
        }

        public async Task CreateIndexRazorFile_WebBlazorClient(ProjectContext projectContext)
        {
            var mainImportsRazorFilePath = Instances.ProjectPathsOperator.GetIndexRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateIndexRazorFile_WebBlazorClient(
                mainImportsRazorFilePath,
                projectContext.ProjectDefaultNamespaceName);
        }

        public async Task CreateMainLayoutRazorFile_WebBlazorClient(ProjectContext projectContext)
        {
            var mainImportsRazorFilePath = Instances.ProjectPathsOperator.GetMainLayoutRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateMainLayoutRazorFile_WebBlazorClient(
                mainImportsRazorFilePath,
				projectContext.ProjectDefaultNamespaceName);
        }

        public async Task CreateInitialWindowsForm(ProjectContext projectContext)
        {
            var formContext = F0089.WinFormContextOperations.Instance.GetWinFormContext(
                projectContext.ProjectFilePath,
                WindowsFormNames.Instance.Form1);

            await Instances.CodeFileOperations.CreateWinForm(formContext);
        }

        public async Task CreateImportsRazorFile_WebBlazorClient_Main(ProjectContext projectContext)
        {
            var mainImportsRazorFilePath = Instances.ProjectPathsOperator.GetMainImportsRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateImportsRazorFile_WebBlazorClient_Main(
                mainImportsRazorFilePath,
				projectContext.ProjectDefaultNamespaceName);
        }

        public async Task CreateIndexRazorFile(ProjectContext projectContext)
        {
            var indexRazorFilePath = Instances.ProjectPathsOperator.GetIndexRazorFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateIndexRazorFile(
                indexRazorFilePath,
                projectContext.ProjectDefaultNamespaceName);
        }

        public async Task CreateAppRazorFile_WebStaticRazorComponents(ProjectContext projectContext)
        {
            var appRazorFilePath = Instances.ProjectPathsOperator.GetAppRazorFilePath_InComponents(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateAppRazorFile_StaticRazorComponents(
                appRazorFilePath,
				projectContext.ProjectDefaultNamespaceName);
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

        public async Task CreateTailwindConfigJsFileWithTypography(ProjectContext projectContext)
        {
            var tailwindConfigJsFilePath = Instances.ProjectPathsOperator.GetTailwindConfigJsFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateTailwindConfigJsWithTypographyFile(
                tailwindConfigJsFilePath);
        }

        public async Task CreateTailwindContentPathsJsonFile(ProjectContext projectContext)
        {
            var tailwindContentPathsJsonFilePath = Instances.ProjectPathsOperator.GetTailwindContentPathsJsonFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateTailwindCssContentPathsJsonFile(
                tailwindContentPathsJsonFilePath);
        }

        public async Task CreateTailwindAllContentPathsJsonFile(ProjectContext projectContext)
        {
            var tailwindContentPathsJsonFilePath = Instances.ProjectPathsOperator.GetTailwindAllContentPathsJsonFilePath(
                projectContext.ProjectFilePath);

            // Initially, just reuse the project's own content paths.
            // A separate script will be required to update all Tailwind CSS content paths.
            await Instances.CodeFileGenerationOperations.CreateTailwindCssContentPathsJsonFile(
                tailwindContentPathsJsonFilePath);
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

        public async Task CreateLaunchSettingsJsonFile_WebServer(ProjectContext projectContext)
        {
            var launchSettingsJsonFilePath = Instances.ProjectPathsOperator.GetLaunchSettingsJsonFilePath(
                projectContext.ProjectFilePath);

            await Instances.CodeFileGenerationOperations.CreateLaunchSettings_WebServer(
                launchSettingsJsonFilePath,
                projectContext.ProjectName);
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

        public Func<ProjectContext, Task> CreateInstance_DeployScripts(string targetProjectName)
        {
            return async projectContext =>
            {
                // Create the default instance.
                var deployScriptsInstanceContext = InstanceTypeContextOperations.Instance.GetInstanceTypeContext(
                    projectContext.ProjectFilePath,
                    InstanceTypeNames.Instance.DeployScripts,
                    InstanceTypes.Instance.Functionality);

                await ProjectOperations.Instance.CreateInstanceInProject(
                        deployScriptsInstanceContext);

                // Then create the actual content.
                await Instances.CodeFileGenerationOperations.CreateInstance_DeployScripts(
                    deployScriptsInstanceContext.InterfaceCodeFilePath,
                    deployScriptsInstanceContext.NamespaceName,
                    targetProjectName);
            };
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

        public async Task CreateInstancesFile_Deploy(ProjectContext projectContext)
        {
            var instancesCodeFilePath = F0052.ProjectPathsOperator.Instance.GetInstancesFilePath(
                projectContext.ProjectFilePath);

            await F0083.CodeFileGenerationOperations.Instance.CreateInstancesClass_Deploy(
                instancesCodeFilePath,
                projectContext.ProjectDefaultNamespaceName);
        }

        public async Task CreateProgramFile_Console(ProjectContext projectContext)
		{
			var programCodeFilePath = F0052.ProjectPathsOperator.Instance.GetProgramFilePath(
				projectContext.ProjectFilePath);

			await F0083.CodeFileGenerationOperations.Instance.CreateProgramFile_Console(
				programCodeFilePath,
				projectContext.ProjectDefaultNamespaceName);
		}

        public async Task CreateProgramFile_DeployScripts(ProjectContext projectContext)
        {
            var programCodeFilePath = F0052.ProjectPathsOperator.Instance.GetProgramFilePath(
                projectContext.ProjectFilePath);

            await F0083.CodeFileGenerationOperations.Instance.CreateProgramFile_DeployScripts(
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

        public async Task CreateStaticRazorComponentsHostFile(ProjectContext projectContext)
        {
            var hostRazorFilePath = F0052.ProjectPathsOperator.Instance.GetStaticRazorComponentsHostFilePath(
                projectContext.ProjectFilePath);

            await F0083.CodeFileGenerationOperations.Instance.CreateStaticRazorComponentsHost(
                hostRazorFilePath,
                projectContext.ProjectDefaultNamespaceName);
        }

        public async Task CreateProgramFile_WebStaticRazorComponents(ProjectContext projectContext)
        {
            var programCodeFilePath = F0052.ProjectPathsOperator.Instance.GetProgramFilePath(
                projectContext.ProjectFilePath);

            await F0083.CodeFileGenerationOperations.Instance.CreateProgramFile_WebStaticRazorComponents(
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

        public async Task CreateProgramFile_WindowsForms(ProjectContext projectContext)
        {
            var programCodeFilePath = F0052.ProjectPathsOperator.Instance.GetProgramFilePath(
                projectContext.ProjectFilePath);

            await F0083.CodeFileGenerationOperations.Instance.CreateProgramFile_WindowsForms(
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

        public async Task RunNpmInstall(ProjectContext projectContext)
        {
            await CliWrap.Cli.Wrap("npm")
                .WithArguments("install -y")
                .WithWorkingDirectory(projectContext.ProjectDirectoryPath)
                .WithConsoleOutput()
                .WithConsoleError()
                .ExecuteAsync();
        }
	}
}