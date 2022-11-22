using System;


namespace R5T.F0084
{
    public static class Instances
    {
        public static F0083.ICodeFileGenerationOperations CodeFileGenerationOperations { get; } = F0083.CodeFileGenerationOperations.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static F0052.IProjectPathsOperator ProjectPathsOperator { get; } = F0052.ProjectPathsOperator.Instance;
        public static F0054.ITextFileGenerator TextFileGenerator { get; } = F0054.TextFileGenerator.Instance;
    }
}