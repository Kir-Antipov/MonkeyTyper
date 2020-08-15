using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MonkeyTyper.WinForms.Data
{
    /// <summary>
    /// Default implementation of the <see cref="IProjectProperties"/>.
    /// </summary>
    /// <remarks>
    /// This implementation uses assembly attributes to get project data.
    /// </remarks>
    internal class AssemblyProjectProperties : IProjectProperties
    {
        #region Var
        /// <inheritdoc />
        public string Copyright { get; }

        /// <inheritdoc />
        public string Authors { get; }

        /// <inheritdoc />
        public string Product { get; }

        /// <inheritdoc />
        public string Version { get; }

        /// <inheritdoc />
        public string ProjectUrl { get; }

        /// <inheritdoc />
        public string HelpUrl { get; }

        private static Regex CopyrightYearExtractor { get; } = new Regex(@"(?:(?<from>\d{4})\s*(?<separator>[-‐—])\s*(?<to>\d{4}))|(?:(?<year>\d{4}))", RegexOptions.Compiled | RegexOptions.Multiline);
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="AssemblyProjectProperties"/> class.
        /// </summary>
        public AssemblyProjectProperties()
        {
            Assembly entry = Assembly.GetEntryAssembly();

            Copyright = ExtractCopyright(entry);
            Authors = ExtractAuthors(entry);
            Product = ExtractProduct(entry);
            Version = ExtractVersion(entry);
            ProjectUrl = ExtractProjectUrl(entry);
            HelpUrl = ExtractHelpUrl(entry);
        }
        #endregion

        #region Functions
        private static string ExtractCopyright(Assembly entry)
        {
            string copyright = entry.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty;
            
            return CopyrightYearExtractor.Replace(copyright, match =>
            {
                string separator = match.Groups["separator"].Value;
                if (string.IsNullOrEmpty(separator))
                    separator = "—";

                if (!int.TryParse(match.Groups["year"].Value, out int year) && !int.TryParse(match.Groups["from"].Value, out year))
                    year = DateTime.Now.Year;

                return year >= DateTime.Now.Year ? $"{year}" : $"{year} {separator} {DateTime.Now.Year}";
            });
        }

        private static string ExtractAuthors(Assembly entry) =>
            entry.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ??
            entry.GetCustomAttribute<AssemblyTrademarkAttribute>()?.Trademark ??
            string.Empty;

        private static string ExtractProduct(Assembly entry) =>
            entry.GetCustomAttribute<AssemblyProductAttribute>()?.Product ??
            entry.GetName().Name;

        private static string ExtractVersion(Assembly entry) =>
            entry.GetCustomAttribute<AssemblyVersionAttribute>()?.Version ??
            entry.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ??
            "1.0.0.0";

        private static string ExtractProjectUrl(Assembly entry) =>
            entry.GetCustomAttribute<AssemblyProjectUrlAttribute>()?.ProjectUrl ??
            string.Empty;

        private static string ExtractHelpUrl(Assembly entry) =>
            entry.GetCustomAttribute<AssemblyHelpUrlAttribute>()?.HelpUrl ??
            string.Empty;
        #endregion
    }
}
