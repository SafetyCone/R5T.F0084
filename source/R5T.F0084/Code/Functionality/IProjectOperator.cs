using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using R5T.F0000;
using R5T.F0081;
using R5T.T0132;
using R5T.T0153;

using R5T.F0084.T001;


namespace R5T.F0084
{
	[FunctionalityMarker]
	public partial interface IProjectOperator : IFunctionalityMarker
	{
        public async Task CreateProject(
            ProjectContext projectContext,
            params Func<ProjectContext, Task>[] projectActions)
        {
            await ActionOperator.Instance.Run(
                projectContext,
                projectActions);
        }

        public async Task CreateProject(
            ProjectContext projectContext,
            IEnumerable<Func<ProjectContext, Task>> projectActions)
        {
            await ActionOperator.Instance.Run(
                projectContext,
                projectActions);
        }

        public async Task CreateProject(
            ProjectContext projectContext,
            Func<XElement> projectElementConstructor,
            IEnumerable<Func<ProjectContext, Task>> projectActions)
        {
            await this.CreateProject(
                projectContext,
                projectActions.Prepend(
                    projectContext => ProjectFileOperator.Instance.CreateProjectFile(
                        projectContext.ProjectFilePath,
                        projectElementConstructor)));
        }

        public async Task CreateProject(
            ProjectContext projectContext,
            Func<string, Task> projectFileConstructor,
            IEnumerable<Func<ProjectContext, Task>> projectActions)
        {
            await this.CreateProject(
                projectContext,
                projectActions.Prepend(
                    projectContext => projectFileConstructor(
                        projectContext.ProjectFilePath)));
        }

        public async Task CreateProject(
            string projectFilePath,
            string projectDescription,
            Func<string, Task> projectFileConstructor,
            IEnumerable<Func<ProjectContext, Task>> projectActions)
        {
            var projectContext = ProjectContextOperator.Instance.GetProjectContext(
                projectFilePath,
                projectDescription);

            await this.CreateProject(
                projectContext,
                projectFileConstructor,
                projectActions);
        }

        public async Task CreateProject(
            string projectFilePath,
            string projectDescription,
            Func<string, Task> projectFileConstructor,
            params Func<ProjectContext, Task>[] projectActions)
        {
            await this.CreateProject(
                projectFilePath,
                projectDescription,
                projectFileConstructor,
                projectActions.AsEnumerable());
        }

        public async Task CreateProject(
            string projectFilePath,
            string projectDescription,
            Func<string, Task> projectFileConstructor,
            Func<IEnumerable<Func<ProjectContext, Task>>> projectActionsConstructor)
        {
            var projectActions = projectActionsConstructor();

            await this.CreateProject(
                projectFilePath,
                projectDescription,
                projectFileConstructor,
                projectActions);
        }

        public async Task CreateProject(
            string projectFilePath,
            string projectDescription,
            Func<string, IEnumerable<Action<XElement>>> projectElementActionsConstructor,
            Func<IEnumerable<Func<ProjectContext, Task>>> projectActionsConstructor)
        {
            await this.CreateProject(
                projectFilePath,
                projectDescription,
                async projectFilePath =>
                {
                    await ProjectFileOperator.Instance.CreateProjectFile(
                        projectFilePath,
                        projectElementActionsConstructor);
                },
                projectActionsConstructor);
        }

        public async Task CreateProject(
            ProjectContext projectContext,
            Func<string, Task> projectFileConstructor,
            Func<IEnumerable<Func<ProjectContext, Task>>> projectActionsConstructor)
        {
            var projectActions = projectActionsConstructor();

            await this.CreateProject(
                projectContext,
                projectFileConstructor,
                projectActions);
        }

        public async Task<string> CreateStronglyTypedType(
            string sourceDirectoryPath,
            string projectName,
            string stronglyTypedTypeTypeName,
            string stronglyTypedTypeDescription,
            Func<string, string, string, string, bool, Task> createStronglyTypedTypeCodeFileFunction,
            bool isDraft = false)
        {
            var projectDefaultNamespaceName = F0040.F000.ProjectNamespacesOperator.Instance.Get_DefaultNamespaceName_FromProjectName(projectName);

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
            var projectDefaultNamespaceName = F0040.F000.ProjectNamespacesOperator.Instance.Get_DefaultNamespaceName_FromProjectFilePath(projectFilePath);

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

            var projectDirectoryPath = F0002.PathOperator.Instance.Get_DirectoryPath(
                parentDirectoryPath,
                projectDirectoryName);

            var projectFilePath = F0052.ProjectPathsOperator.Instance.Get_ProjectFilePath(
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