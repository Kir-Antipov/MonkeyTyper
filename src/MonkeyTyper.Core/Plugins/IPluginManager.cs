using System.Collections.Generic;
using System.IO;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Describes methods for interacting with custom plugins.
    /// </summary>
    public interface IPluginManager
    {
        /// <summary>
        /// Loads plugins from the default location.
        /// </summary>
        /// <returns>
        /// Descriptors of services loaded from plugins.
        /// </returns>
        IEnumerable<ServiceDescriptor> Load();

        /// <summary>
        /// Loads plugins from the specified files.
        /// </summary>
        /// <param name="files">
        /// File paths containing plugin assemblies.
        /// </param>
        /// <returns>
        /// Descriptors of services loaded from plugins.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        /// One of the specified files was not found.
        /// </exception>
        IEnumerable<ServiceDescriptor> Load(IEnumerable<string> files);
    }
}
