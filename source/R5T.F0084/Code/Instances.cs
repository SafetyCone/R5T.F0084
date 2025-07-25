using System;


namespace R5T.F0084
{
    public static class Instances
    {
        public static F0094.ICodeFileGenerationOperations CodeFileGenerationOperations => F0094.CodeFileGenerationOperations.Instance;
        public static F0094.ICodeFileOperations CodeFileOperations => F0094.CodeFileOperations.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator => F0000.FileSystemOperator.Instance;
        public static F0089.IInstanceTypeContextOperations InstanceTypeContextOperations => F0089.InstanceTypeContextOperations.Instance;
        public static F0032.IJsonOperator JsonOperator => F0032.JsonOperator.Instance;
        public static F0052.IProjectPathsOperator ProjectPathsOperator => F0052.ProjectPathsOperator.Instance;
        public static F0054.ITextFileGenerator TextFileGenerator => F0054.TextFileGenerator.Instance;
    }
}