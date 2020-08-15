namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    internal class AssemblyHelpUrlAttribute : Attribute
    {
        public string HelpUrl { get; }

        public AssemblyHelpUrlAttribute(string helpUrl) => HelpUrl = helpUrl;
    }
}
