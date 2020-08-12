using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Default implementation of the <see cref="IPluginManager"/>.
    /// </summary>
    public class DefaultPluginManager : IPluginManager
    {
        private const string DefaultPluginLocation = "plugins";

        /// <inheritdoc cref="IPluginManager.Load"/>
        public virtual IEnumerable<ServiceDescriptor> Load() => Directory.Exists(DefaultPluginLocation) ?
            Load(Directory.EnumerateFiles(DefaultPluginLocation, "*.dll", SearchOption.TopDirectoryOnly)) :
            Array.Empty<ServiceDescriptor>();

        /// <inheritdoc cref="IPluginManager.Load(IEnumerable{string})"/>
        public virtual IEnumerable<ServiceDescriptor> Load(IEnumerable<string> files) => (files ?? throw new ArgumentNullException(nameof(files)))
            .Select(x =>
            {
                if (!File.Exists(x))
                    throw new FileNotFoundException("The specified file was not found.", x);

                try
                {
                    return Assembly.Load(File.ReadAllBytes(x));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return null;
                }
            })
            .Where(x => x is { })
            .SelectMany(x =>
            {
                try
                {
                    return x!.ExportedTypes.Where(x => x.GetCustomAttribute<ServiceAttribute>() is { });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return Type.EmptyTypes;
                }
            })
            .Select(x =>
            {
                ServiceAttribute attribute = x.GetCustomAttribute<ServiceAttribute>();
                Type? settingsType = attribute.SettingsType is { } && typeof(ISettings).IsAssignableFrom(attribute.SettingsType) ?
                    attribute.SettingsType :
                    default;

                return new ServiceDescriptor(attribute.ServiceType ?? x, x, attribute.Lifetime, settingsType);
            });
    }
}
