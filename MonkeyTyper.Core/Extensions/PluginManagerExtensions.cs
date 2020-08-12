using MonkeyTyper.Core.Plugins;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonkeyTyper.Core.Extensions
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) helpful methods for objects
    /// that implement <see cref="IPluginManager"/>.
    /// </summary>
    public static class PluginManagerExtensions
    {
        /// <inheritdoc cref="Load(IPluginManager, string, string, SearchOption)"/>
        public static IEnumerable<ServiceDescriptor> Load(this IPluginManager storage, string path)
        {
            _ = storage ?? throw new ArgumentNullException(nameof(storage));
            _ = path ?? throw new ArgumentNullException(nameof(path));

            return storage.Load(Directory.EnumerateFiles(path));
        }

        /// <inheritdoc cref="Load(IPluginManager, string, string, SearchOption)"/>
        public static IEnumerable<ServiceDescriptor> Load(this IPluginManager storage, string path, string searchPattern)
        {
            _ = storage ?? throw new ArgumentNullException(nameof(storage));
            _ = path ?? throw new ArgumentNullException(nameof(path));
            _ = searchPattern ?? throw new ArgumentNullException(nameof(searchPattern));

            return storage.Load(Directory.EnumerateFiles(path, searchPattern));
        }

        /// <summary>
        /// Loads plugins from the specified directory.
        /// </summary>
        /// <param name="storage">The <see cref="IPluginManager"/> instance.</param>
        /// <param name="path"> The relative or absolute path to the directory to search.</param>
        /// <param name="searchPattern">
        /// The search string to match against the names of files in path. This parameter
        /// can contain a combination of valid literal path and wildcard (* and ?) characters,
        /// but it doesn't support regular expressions.
        /// </param>
        /// <param name="options">
        /// One of the enumeration values that specifies whether the search operation should
        /// include only the current directory or should include all subdirectories.
        /// The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        /// Descriptors of services loaded from plugins.
        /// </returns>
        public static IEnumerable<ServiceDescriptor> Load(this IPluginManager storage, string path, string searchPattern, SearchOption options)
        {
            _ = storage ?? throw new ArgumentNullException(nameof(storage));
            _ = path ?? throw new ArgumentNullException(nameof(path));
            _ = searchPattern ?? throw new ArgumentNullException(nameof(searchPattern));

            return storage.Load(Directory.EnumerateFiles(path, searchPattern, options));
        }

#if !NETSTANDARD2_0
        /// <param name="options">Enumeration options.</param>
        /// <inheritdoc cref="Load(IPluginManager, string, string, SearchOption)"/>
        public static IEnumerable<ServiceDescriptor> Load(this IPluginManager storage, string path, string searchPattern, EnumerationOptions options)
        {
            _ = storage ?? throw new ArgumentNullException(nameof(storage));
            _ = path ?? throw new ArgumentNullException(nameof(path));
            _ = searchPattern ?? throw new ArgumentNullException(nameof(searchPattern));
            _ = options ?? throw new ArgumentNullException(nameof(options));

            return storage.Load(Directory.EnumerateFiles(path, searchPattern, options));
        }
#endif
    }
}
