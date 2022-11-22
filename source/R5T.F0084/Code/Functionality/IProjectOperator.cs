using System;
using System.Threading.Tasks;

using R5T.T0132;

using R5T.F0084.T001;
using R5T.F0084.T002;


namespace R5T.F0084
{
	[FunctionalityMarker]
	public partial interface IProjectOperator : IFunctionalityMarker
	{
        public async Task CreateNewProject(
            string projectFilePath,
            Func<string, Task> projectFileGenerator,
            Func<string, Task> projectGenerator)
        {
            await projectFileGenerator(projectFilePath);

            await projectGenerator(projectFilePath);
        }

        public async Task<ProjectContext> CreateNewProject(
            string projectFilePath,
            string projectDescription,
            Func<string, Task> projectFileGenerator,
            Func<ProjectContext, Task> projectGenerator)
        {
            await projectFileGenerator(projectFilePath);

            var projectContext = ProjectContextOperator.Instance.GetProjectContext(
                projectFilePath,
                projectDescription);

            await projectGenerator(projectContext);

            return projectContext;
        }

        public async Task<string> CreateStronglyTypedType(
            string sourceDirectoryPath,
            string projectName,
            string stronglyTypedTypeTypeName,
            string stronglyTypedTypeDescription,
            Func<string, string, string, string, bool, Task> createStronglyTypedTypeCodeFileFunction,
            bool isDraft = false)
        {
            var projectDefaultNamespaceName = F0040.F000.ProjectNamespacesOperator.Instance.GetDefaultNamespaceName_FromProjectName(projectName);

            string stronglyTypedGuidCodeFilePath = default;

            await ProjectOperator.Instance.InProjectPathInformationContext(
                sourceDirectoryPath,
                projectName,
                async projectPathInformation =>
                {
                    // Create the code file.
                    stronglyTypedGuidCodeFilePath = F0052.ProjectPathsOperator.Instance.GetStrongTypeCodeFilePath(
                        projectPathInformation.ProjectFilePath,
                        stronglyTypedTypeTypeName);

                    await createStronglyTypedTypeCodeFileFunction(
                        stronglyTypedGuidCodeFilePath,
                        projectDefaultNamespaceName,
                        stronglyTypedTypeTypeName,
                        stronglyTypedTypeDescription,
                        isDraft);

                    // Ensure the project has the required references.
                    F0020.ProjectFileOperator.Instance.AddProjectReferences_Idempotent_Synchronous(
                        projectPathInformation.ProjectFilePath,
                        Z0018.ProjectFilePaths.Instance.StronglyTypedTypeDependencies);
                });

            return stronglyTypedGuidCodeFilePath;
        }

        public async Task<string> CreateStronglyTypedType(
            string projectFilePath,
            string stronglyTypedTypeTypeName,
            string stronglyTypedTypeDescription,
            Func<string, string, string, string, bool, Task> createStronglyTypedTypeCodeFileFunction,
            bool isDraft = false)
        {
            var projectDefaultNamespaceName = F0040.F000.ProjectNamespacesOperator.Instance.GetDefaultNamespaceName_FromProjectFilePath(projectFilePath);

            string stronglyTypedGuidCodeFilePath = default;

            await ProjectOperator.Instance.InProjectPathInformationContext(
                projectFilePath,
                async projectPathInformation =>
                {
                    // Create the code file.
                    stronglyTypedGuidCodeFilePath = F0052.ProjectPathsOperator.Instance.GetStrongTypeCodeFilePath(
                        projectPathInformation.ProjectFilePath,
                        stronglyTypedTypeTypeName);

                    await createStronglyTypedTypeCodeFileFunction(
                        stronglyTypedGuidCodeFilePath,
                        projectDefaultNamespaceName,
                        stronglyTypedTypeTypeName,
                        stronglyTypedTypeDescription,
                        isDraft);

                    // Ensure the project has the required references.
                    F0020.ProjectFileOperator.Instance.AddProjectReferences_Idempotent_Synchronous(
                        projectPathInformation.ProjectFilePath,
                        Z0018.ProjectFilePaths.Instance.StronglyTypedTypeDependencies);
                });

            return stronglyTypedGuidCodeFilePath;
        }

        public async Task<ProjectPathInformation> InProjectPathInformationContext(
            string parentDirectoryPath,
            string projectName,
            Func<ProjectPathInformation, Task> projectDirectoryPathAction)
        {
            var projectDirectoryName = F0052.ProjectDirectoryNameOperator.Instance.GetProjectDirectoryName_FromProjectName(projectName);

            var projectDirectoryPath = F0002.PathOperator.Instance.GetDirectoryPath(
                parentDirectoryPath,
                projectDirectoryName);

            var projectFilePath = F0052.ProjectPathsOperator.Instance.GetProjectFilePath(
                    projectDirectoryPath,
                    projectName);

            var projectPathInformation = new ProjectPathInformation
            {
                ProjectDirectoryPath = projectDirectoryPath,
                ProjectFilePath = projectFilePath,
            };

            await projectDirectoryPathAction(projectPathInformation);

            return projectPathInformation;
        }

        public async Task<ProjectPathInformation> InProjectPathInformationContext(
            string projectFilePath,
            Func<ProjectPathInformation, Task> projectDirectoryPathAction)
        {
            var projectDirectoryPath = F0052.ProjectPathsOperator.Instance.GetProjectDirectoryPath(projectFilePath);

            var projectPathInformation = new ProjectPathInformation
            {
                ProjectDirectoryPath = projectDirectoryPath,
                ProjectFilePath = projectFilePath,
            };

            await projectDirectoryPathAction(projectPathInformation);

            return projectPathInformation;
        }
    }
}