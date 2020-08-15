namespace MonkeyTyper.WinForms.Data
{
    /// <summary>
    /// Describes project properties.
    /// </summary>
    public interface IProjectProperties
    {
        /// <summary>
        /// Project's copyright.
        /// </summary>
        string Copyright { get; }

        /// <summary>
        /// Project's authors.
        /// </summary>
        string Authors { get; }

        /// <summary>
        /// Project's name.
        /// </summary>
        string Product { get; }

        /// <summary>
        /// Project's version.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Project's website url.
        /// </summary>
        string ProjectUrl { get; }

        /// <summary>
        /// Project's support url.
        /// </summary>
        string HelpUrl { get; }
    }
}
