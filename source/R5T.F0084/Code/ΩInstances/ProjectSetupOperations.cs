using System;


namespace R5T.F0084
{
    public class ProjectSetupOperations : IProjectSetupOperations
    {
        #region Infrastructure

        public static IProjectSetupOperations Instance { get; } = new ProjectSetupOperations();


        private ProjectSetupOperations()
        {
        }

        #endregion
    }
}
