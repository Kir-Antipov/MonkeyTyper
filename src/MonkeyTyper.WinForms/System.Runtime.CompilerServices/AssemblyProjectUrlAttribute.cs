namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    internal class AssemblyProjectUrlAttribute : Attribute
    {
        public string ProjectUrl { get; }

        public AssemblyProjectUrlAttribute(string projectUrl) => ProjectUrl = projectUrl;
    }
}
