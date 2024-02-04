using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.F0083;
using R5T.L0026.T000;
using R5T.T0132;
using R5T.T0153;
using R5T.T0153.N000;


namespace R5T.F0084
{
	[FunctionalityMarker]
	public partial interface IProjectOperations : IFunctionalityMarker
	{
        public async Task<WinFormContext> CreateWinFormInProject(
            string projectFilePath,
            string formName)
        {
            var winFormContext = await Instances.CodeFileOperations.CreateWinForm(
                projectFilePath,
                formName);

            return winFormContext;
        }

        public async Task CreateWinFormInProject(
            WinFormContext winFormContext)
        {
            await Instances.CodeFileOperations.CreateWinForm(
                winFormContext);
        }

        public async Task<Dictionary<string, InstanceTypeContext>> CreateInstancesInProject(
            string projectFilePath,
            InstanceTypeInformation instanceType,
            IEnumerable<string> instanceTypeNameStems)
        {
            var output = new Dictionary<string, InstanceTypeContext>();

            foreach (var instanceTypeNameStem in instanceTypeNameStems)
            {
                var instanceTypeContext = F0089.InstanceTypeContextOperations.Instance.GetInstanceTypeContext(
                    projectFilePath,
                    instanceTypeNameStem,
                    instanceType);

                await this.CreateInstanceInProject(instanceTypeContext);

                output.Add(instanceTypeNameStem, instanceTypeContext);
            }

            return output;
        }

        public async Task CreateInstanceInProject(
            InstanceTypeContext instanceTypeContext)
        {
            // Safety checks.
            F0000.FileSystemOperator.Instance.Verify_File_DoesNotExist(instanceTypeContext.InterfaceCodeFilePath);
            // Don't care about the class.

            await CodeFileGenerationOperations.Instance.CreateInstanceInterfaceCodeFile(
                instanceTypeContext.InterfaceCodeFilePath,
                instanceTypeContext.NamespaceName,
                instanceTypeContext.InterfaceTypeName,
                instanceTypeContext.MarkerAttributeTypeName,
                instanceTypeContext.MarkerInterfaceTypeName,
                [
                    instanceTypeContext.MarkerAttributeNamespaceName,
                    instanceTypeContext.MarkerInterfaceNamespaceName,
                ]);

            await CodeFileGenerationOperations.Instance.CreateInstanceClassCodeFile(
                instanceTypeContext.ClassCodeFilePath,
                instanceTypeContext.NamespaceName,
                instanceTypeContext.ClassTypeName,
                instanceTypeContext.InterfaceTypeName);
        }

        public async Task<RazorComponentContext> CreateRazorComponentInProject(
            string projectFilePath,
            string componentName)
        {
            var razorComponentContext = F0089.CodeFileContextOperations.Instance.GetRazorComponentContext(
                projectFilePath,
                componentName);

            await this.CreateRazorComponentInProject(
                razorComponentContext);

            return razorComponentContext;
        }

        public async Task CreateRazorComponentInProject(
            RazorComponentContext razorComponentContext)
        {
            // Safety checks.
            F0000.FileSystemOperator.Instance.Verify_File_DoesNotExist(razorComponentContext.RazorFilePath);
            F0000.FileSystemOperator.Instance.Verify_File_DoesNotExist(razorComponentContext.CodeBehindFilePath);

            // Run.
            await CodeFileGenerationOperations.Instance.CreateRazorComponentMarkupFile(
                razorComponentContext.RazorFilePath,
                razorComponentContext.NamespaceName);

            await CodeFileGenerationOperations.Instance.CreateRazorComponentCodeBehindFile(
                razorComponentContext.CodeBehindFilePath,
                razorComponentContext.NamespaceName,
                razorComponentContext.Name);
        }

        public async Task CreateInstancesClassInProject(
            string projectFilePath)
        {
            var namespaceName = F0040.F000.ProjectNamespacesOperator.Instance.Get_DefaultNamespaceName_FromProjectFilePath(projectFilePath);

            var instancesCodeFilePath = F0052.ProjectPathsOperator.Instance.GetInstancesFilePath(projectFilePath);

            // Safety check.
            F0000.FileSystemOperator.Instance.Verify_File_DoesNotExist(instancesCodeFilePath);

            await CodeFileGenerationOperations.Instance.CreateInstancesClass(
                instancesCodeFilePath,
                namespaceName);
        }

		/// <inheritdoc cref="CreateClassInProject(TypeContext)" path="/summary"/>
		/// <returns>The C# code file path of the class.</returns>
		public async Task<string> CreateClassInProject(
			string projectFilePath,
			string className)
		{
            var namespaceName = F0040.F000.ProjectNamespacesOperator.Instance.Get_DefaultNamespaceName_FromProjectFilePath(projectFilePath);

            var classFilePath = F0052.ProjectPathsOperator.Instance.GetClassCodeFilePath(
                projectFilePath,
                className);

			// Safety check.
			F0000.FileSystemOperator.Instance.Verify_File_DoesNotExist(classFilePath);

			// Now run.
            await CodeFileGenerationOperations.Instance.CreateClassCSharpFile(
                classFilePath,
                namespaceName,
                className);

			return classFilePath;
        }

        /// <summary>
        /// Creates a class in a C# code file in a project.
        /// </summary>
        public async Task CreateClassInProject(
            TypeContext typeContext)
        {
            // Safety check.
            F0000.FileSystemOperator.Instance.Verify_File_DoesNotExist(
                typeContext.CodeFilePath);

            // Now run.
            await CodeFileGenerationOperations.Instance.CreateClassCSharpFile(
                typeContext.CodeFilePath,
                typeContext.NamespaceName,
                typeContext.TypeName);
        }

        /// <summary>
        /// Creates an interface in a C# code file in a project.
        /// </summary>
        public async Task CreateInterfaceInProject(
            TypeContext typeContext)
        {
            // Safety check.
            F0000.FileSystemOperator.Instance.Verify_File_DoesNotExist(
                typeContext.CodeFilePath);

            // Now run.
            await CodeFileGenerationOperations.Instance.CreateInterfaceCSharpFile(
                typeContext.CodeFilePath,
                typeContext.NamespaceName,
                typeContext.TypeName);
        }

        public async Task NewProject_WebApplicationServer(
			string projectFilePath,
			string projectDescription)
		{
			await ProjectOperator.Instance.CreateProject(
				projectFilePath,
				projectDescription,
				F0081.ProjectFileOperations.Instance.NewProjectFile_Web,
                ProjectSetupOperations.Instance.SetupProject_Library);
		}

		public async Task NewProject_Library(
			string projectFilePath,
			string projectDescription)
		{
			await ProjectOperator.Instance.CreateProject(
				projectFilePath,
				projectDescription,
				F0081.ProjectFileOperations.Instance.NewProjectFile_Library,
                ProjectSetupOperations.Instance.SetupProject_Library);
		}

        public Task NewProject_Library(
            ProjectContext projectContext)
        {
            return ProjectOperator.Instance.CreateProject(
                projectContext,
                F0081.ProjectFileOperations.Instance.NewProjectFile_Library,
                ProjectSetupOperations.Instance.SetupProject_Library);
        }

        public async Task NewProject_Console(
			string projectFilePath,
			string projectDescription)
		{
			await ProjectOperator.Instance.CreateProject(
				projectFilePath,
				projectDescription,
				F0081.ProjectFileOperations.Instance.NewProjectFile_Console,
                ProjectSetupOperations.Instance.SetupProject_Console);
		}

        public async Task NewProject_DeployScripts(
            string projectFilePath,
            string projectDescription,
            string targetProjectName)
        {
            await ProjectOperator.Instance.CreateProject(
                projectFilePath,
                projectDescription,
                F0081.ProjectFileOperations.Instance.NewProjectFile_DeployScripts,
                ProjectSetupOperations.Instance.SetupProject_DeployScripts(targetProjectName));
        }

        public async Task NewProject_RazorClassLibrary(
            string projectFilePath,
            string projectDescription)
        {
            await ProjectOperator.Instance.CreateProject(
                projectFilePath,
                projectDescription,
                F0081.ProjectFileOperations.Instance.NewProjectFile_RazorClassLibrary,
                ProjectSetupOperations.Instance.SetupProject_RazorClassLibrary);
        }

        public Task NewProject_RazorClassLibrary(
            ProjectContext projectContext)
        {
            return ProjectOperator.Instance.CreateProject(
                projectContext,
                F0081.ProjectFileOperations.Instance.NewProjectFile_RazorClassLibrary,
                ProjectSetupOperations.Instance.SetupProject_RazorClassLibrary);
        }

        public async Task NewProject_WebServerForBlazorClient(
			string projectFilePath,
			string projectDescription)
		{
			await ProjectOperator.Instance.CreateProject(
				projectFilePath,
				projectDescription,
				F0081.ProjectFileOperations.Instance.NewProjectFile_WebServerForBlazorClient,
                ProjectSetupOperations.Instance.SetupProject_WebServerForBlazorClient);
		}

        public async Task NewProject_Blog(
            string projectFilePath,
            string projectDescription)
        {
            await ProjectOperator.Instance.CreateProject(
                projectFilePath,
                projectDescription,
                F0081.ProjectFileOperations.Instance.NewProjectFile_Blog,
                ProjectSetupOperations.Instance.SetupProject_Blog);
        }

        public async Task NewProject_WebStaticRazorComponents(
            string projectFilePath,
            string projectDescription)
        {
            await ProjectOperator.Instance.CreateProject(
                projectFilePath,
                projectDescription,
                F0081.ProjectFileOperations.Instance.NewProjectFile_WebStaticRazorComponents,
                ProjectSetupOperations.Instance.SetupProject_WebStaticRazorComponents);
        }

        public async Task NewProject_WindowsFormsApplication(
            string projectFilePath,
            string projectDescription)
        {
            await ProjectOperator.Instance.CreateProject(
                projectFilePath,
                projectDescription,
                F0081.ProjectFileOperations.Instance.NewProjectFile_WindowsFormsApplication,
                ProjectSetupOperations.Instance.SetupProject_WindowsFormsApplication);
        }

        public async Task NewProject_WebBlazorClient(
            string projectFilePath,
            string projectDescription)
        {
            await ProjectOperator.Instance.CreateProject(
                projectFilePath,
                projectDescription,
                F0081.ProjectFileOperations.Instance.NewProjectFile_WebBlazorClient,
                ProjectSetupOperations.Instance.SetupProject_WebBlazorClient);
        }
    }
}