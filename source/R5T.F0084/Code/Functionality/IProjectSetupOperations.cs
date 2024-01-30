using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0132;
using R5T.T0153;


namespace R5T.F0084
{
    [FunctionalityMarker]
    public partial interface IProjectSetupOperations : IFunctionalityMarker
    {
        private static IProjectFileSystemOperations FileSystem { get; } = ProjectFileSystemOperations.Instance;


        public IEnumerable<Func<ProjectContext, Task>> SetupProject_RazorClassLibrary()
        {
            var output = this.SetupProject_Initial()
                .Append(
                    FileSystem.CreateComponentsDirectory,
                    FileSystem.CreateExampleComponent,
                    FileSystem.CreateWwwRootDirectory,
                    projectContext =>
                    {
                        // Create something in the wwwroot directory so it is not empty (to make sure the directory is not hidden in the Visual Studio solution explorer).
                        var wwwRootPlaceholderFilePath = F0002.PathOperator.Instance.Get_FilePath(
                            Instances.ProjectPathsOperator.GetWwwRootDirectoryPath(
                                projectContext.ProjectFilePath),
                            "Placeholder.html");

                        F0000.FileOperator.Instance.Write_Text_Synchronous(
                            wwwRootPlaceholderFilePath,
                            "Placeholder text...");

                        return Task.CompletedTask;
                    },
                    FileSystem.CreateTailwindContentPathsJsonFile)
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_Console()
        {
            var output = this.SetupProject_Initial()
                .Append(
                    FileSystem.CreateProgramFile_Console)
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_DeployScripts(string targetProjectName)
        {
            var output = this.SetupProject_Initial()
                .Append(
                    FileSystem.CreateProgramFile_DeployScripts,
                    FileSystem.CreateInstance_DeployScripts(targetProjectName),
                    FileSystem.CreateInstancesFile_Deploy)
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_WebServerForBlazorClient()
        {
            var output = this.SetupProject_Initial()
                .Append(
                    FileSystem.CreatePropertiesDirectory,
                    FileSystem.CreateLaunchSettingsJsonFile_WebServerForBlazorClient,
                    FileSystem.CreateAppSettingsJsonFile,
                    FileSystem.CreateAppSettingsDevelopmentJsonFile,
                    FileSystem.CreateProgramFile_WebServerForBlazorClient)
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_WindowsFormsApplication()
        {
            var output = this.SetupProject_Initial()
                .Append(
                    FileSystem.CreateInitialWindowsForm,
                    FileSystem.CreateProgramFile_WindowsForms)
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_Blog()
        {
            var output = this.SetupProject_WebStaticRazorComponents()
                .Append(
                    FileSystem.AddTailwindCssTypograpy,
                    FileSystem.CreateTailwindConfigJsFileWithTypography)
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_WebStaticRazorComponents()
        {
            var output = this.SetupProject_Initial()
                .Append(
                    FileSystem.CreatePropertiesDirectory,
                    FileSystem.CreateLaunchSettingsJsonFile_WebServer,
                    FileSystem.CreateAppSettingsJsonFile,
                    FileSystem.CreateAppSettingsDevelopmentJsonFile,
                    FileSystem.CreateProgramFile_WebStaticRazorComponents,
                    FileSystem.CreatePagesDirectory,
                    FileSystem.CreateComponentsDirectory,
                    FileSystem.CreateStaticRazorComponentsHostFile,
                    FileSystem.CreateAppRazorFile_WebStaticRazorComponents,
                    FileSystem.CreateIndexRazorFile,
                    // For some reason, the WebApplication.CreateBuilder() call fails if there is no wwwroot directory.
                    FileSystem.CreateWwwRootDirectory,
                    FileSystem.CreatePackageJsonFile,
                    FileSystem.CreateTailwindAllContentPathsJsonFile,
                    FileSystem.CreateTailwindContentPathsJsonFile,
                    FileSystem.CreateTailwindConfigJsFile,
                    FileSystem.CreateTailwindCssFile,
                    FileSystem.RunNpmInstall)
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_WebBlazorClient()
        {
            var output = this.SetupProject_Initial()
                .Append(
                    FileSystem.CreatePropertiesDirectory,
                    FileSystem.CreateLaunchSettingsJsonFile_WebServerForBlazorClient,
                    FileSystem.CreatePackageJsonFile,
                    FileSystem.CreateTailwindConfigJsFile,
                    FileSystem.CreateTailwindCssFile,
                    FileSystem.CreateWwwRootDirectory,
                    FileSystem.CreateIndexHtmlFile,
                    FileSystem.CreatePagesDirectory,
                    FileSystem.CreateSharedDirectory,
                    FileSystem.CreateComponentsDirectory,
                    FileSystem.CreateProgramFile_WebBlazorClient,
                    FileSystem.CreateAppRazorFile_WebBlazorClient,
                    FileSystem.CreateImportsRazorFile_WebBlazorClient_Main,
                    FileSystem.CreateMainLayoutRazorFile_WebBlazorClient,
                    FileSystem.CreateIndexRazorFile_WebBlazorClient,
                    FileSystem.CreateTailwindContentPathsJsonFile,
                    FileSystem.CreateTailwindAllContentPathsJsonFile,
                    FileSystem.RunNpmInstall)
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_Library()
        {
            var output = this.SetupProject_Initial()
                // No changes needed.
                ;

            return output;
        }

        public IEnumerable<Func<ProjectContext, Task>> SetupProject_Initial()
        {
            var output = new[]
            {
                FileSystem.CreateProjectPlanMarkdownFile,
                FileSystem.CreateCodeDirectory,
                FileSystem.CreateInstancesFile,
                FileSystem.CreateDocumentationFile,
            };

            return output;
        }
    }
}
