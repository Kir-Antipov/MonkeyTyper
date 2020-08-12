using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Default implementation of the <see cref="ISettingsProvider"/>.
    /// </summary>
    public class DefaultSettingsProvider : ISettingsProvider
    {
        #region Var
        private const string DefaultSettingsLocation = "settings.json";

        private Dictionary<Type, ISettings> TypeSettings { get; } = new Dictionary<Type, ISettings>();
        private Dictionary<Guid, ISettings> GuidSettings { get; } = new Dictionary<Guid, ISettings>();

        private ISettings? AppSettings { get; set; }

        /// <inheritdoc cref="ISettingsProvider.Settings"/>
        public IEnumerable<ISettings> Settings
        {
            get
            {
                if (AppSettings is { })
                    yield return AppSettings;

                foreach (ISettings settings in TypeSettings.Values)
                    if (!ReferenceEquals(AppSettings, settings))
                        yield return settings;
            }
        }
        #endregion

        #region Functions
        /// <inheritdoc cref="ISettingsProvider.RegisterAppSettings(Type)"/>
        public void RegisterAppSettings(Type settingsType)
        {
            _ = settingsType ?? throw new ArgumentNullException(nameof(settingsType));
            if (!typeof(ISettings).IsAssignableFrom(settingsType))
                throw new InvalidCastException();

            AppSettings = (ISettings)Activator.CreateInstance(settingsType);
            TypeSettings[settingsType] = AppSettings;
            GuidSettings[AppSettings.Guid] = AppSettings;
        }

        /// <inheritdoc cref="ISettingsProvider.Register(Type)"/>
        public void Register(Type settingsType)
        {
            _ = settingsType ?? throw new ArgumentNullException(nameof(settingsType));
            if (!typeof(ISettings).IsAssignableFrom(settingsType))
                throw new InvalidCastException();

            ISettings settings = (ISettings)Activator.CreateInstance(settingsType);
            TypeSettings[settingsType] = settings;
            GuidSettings[settings.Guid] = settings;
        }

        /// <inheritdoc cref="ISettingsProvider.Store()"/>
        public void Store() => Store(DefaultSettingsLocation);

        /// <inheritdoc cref="ISettingsProvider.Store(string)"/>
        public void Store(string filename)
        {
            Dictionary<string, Dictionary<string, object?>> data = TypeSettings.Values
                .ToDictionary(x => x.Guid.ToString(), x => new Dictionary<string, object?>(x));

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(filename, json);
        }

        /// <inheritdoc cref="ISettingsProvider.Restore()"/>
        public void Restore()
        {
            if (File.Exists(DefaultSettingsLocation))
                Restore(DefaultSettingsLocation);
        }

        /// <inheritdoc cref="ISettingsProvider.Restore(string)"/>
        public void Restore(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("The specified file was not found.", filename);

            try
            {
                JObject root = JObject.Parse(File.ReadAllText(filename));

                foreach (var entry in root)
                {
                    if (!(entry.Value is { Type: JTokenType.Object }))
                        continue;

                    if (!Guid.TryParse(entry.Key, out Guid guid))
                        continue;

                    if (!GuidSettings.TryGetValue(guid, out ISettings? settings))
                        continue;

                    JsonConvert.PopulateObject(entry.Value.ToString(), settings);
                }
            }
            catch (Exception ex)
            {
                throw new FormatException("The specified file had an incorrect format.", ex);
            }
        }

        /// <inheritdoc cref="ISettingsProvider.Get(Type)"/>
        public ISettings? Get(Type settingsType) =>
            TypeSettings.TryGetValue(settingsType ?? throw new ArgumentNullException(nameof(settingsType)), out ISettings? settings) ? settings : default;
        #endregion
    }
}
