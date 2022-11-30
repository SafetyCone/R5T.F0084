using System;

using R5T.T0142;


namespace R5T.F0084.T002
{
    [DataTypeMarker]
    public class ProjectContext : ProjectFileContext
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectDefaultNamespaceName { get; set; }
    }
}
