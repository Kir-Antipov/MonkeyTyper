using MonkeyTyper.Core.Plugins;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MonkeyTyper.Core.Extensions
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) helpful methods for objects
    /// that implement <see cref="ISettingsProvider"/>.
    /// </summary>
    public static class SettingsProviderExtensions
    {
        /// <summary>
        /// Adds support for plugin settings.
        /// </summary>
        /// <typeparam name="TSettings">The type of plugin's settings.</typeparam>
        /// <param name="provider">The <see cref="ISettingsProvider"/> instance.</param>
        public static void Register<TSettings>(this ISettingsProvider provider) where TSettings : ISettings, new()
        {
            _ = provider ?? throw new ArgumentNullException(nameof(provider));
            provider.Register(typeof(TSettings));
        }

        /// <summary>
        /// Adds support for application settings.
        /// </summary>
        /// <typeparam name="TSettings">The type of application's settings.</typeparam>
        /// <param name="provider">The <see cref="ISettingsProvider"/> instance.</param>
        public static void RegisterAppSettings<TSettings>(this ISettingsProvider provider) where TSettings : ISettings, new()
        {
            _ = provider ?? throw new ArgumentNullException(nameof(provider));
            provider.RegisterAppSettings(typeof(TSettings));
        }

        /// <summary>
        /// Gets a settings object that matches the given type.
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <param name="provider">The type of the settings.</param>
        /// <returns>
        /// The <typeparamref name="TSettings"/> instance if presented;
        /// otherwise, <see langword="null"/>.
        /// </returns>
        [return: MaybeNull]
        public static TSettings Get<TSettings>(this ISettingsProvider provider) where TSettings : ISettings, new()
        {
            _ = provider ?? throw new ArgumentNullException(nameof(provider));

            return provider.Get(typeof(TSettings)) is TSettings settings ? settings : default;
        }
    }
}
