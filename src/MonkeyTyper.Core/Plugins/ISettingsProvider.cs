using System.IO;
using System;
using System.Collections.Generic;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Provides access to application/plugins settings.
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Returns all available settings.
        /// </summary>
        /// <remarks>
        /// App settings are always returned first if any registered.
        /// </remarks>
        IEnumerable<ISettings> Settings { get; }

        /// <summary>
        /// Adds support for plugin settings.
        /// </summary>
        /// <param name="settingsType">
        /// The <see cref="Type"/> of plugin's settings.
        /// </param>
        /// <exception cref="InvalidCastException">
        /// The specified type does not implement
        /// the <see cref="ISettings"/> interface.
        /// </exception>
        void Register(Type settingsType);

        /// <summary>
        /// Adds support for application settings.
        /// </summary>
        /// <param name="settingsType">
        /// The <see cref="Type"/> of application's settings.
        /// </param>
        /// <exception cref="InvalidCastException">
        /// The specified type does not implement
        /// the <see cref="ISettings"/> interface.
        /// </exception>
        void RegisterAppSettings(Type settingsType);

        /// <summary>
        /// Stores the current settings' state to the default location.
        /// </summary>
        void Store();

        /// <summary>
        /// Stores the current settings' state to the specified file.
        /// </summary>
        void Store(string filename);

        /// <summary>
        /// Restores the settings' state from the default location.
        /// </summary>
        void Restore();

        /// <summary>
        /// Restores the settings' state from the specified file.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The specified file was not found.
        /// </exception>
        /// <exception cref="FormatException">
        /// The specified file had incorrect format.
        /// </exception>
        void Restore(string filename);

        /// <summary>
        /// Gets a settings object that matches the given type.
        /// </summary>
        /// <param name="settingsType">
        /// The <see cref="Type"/> of the settings.
        /// </param>
        /// <returns>
        /// The <see cref="ISettings"/> instance if presented; otherwise, <see langword="null"/>.
        /// </returns>
        ISettings? Get(Type settingsType);
    }
}
