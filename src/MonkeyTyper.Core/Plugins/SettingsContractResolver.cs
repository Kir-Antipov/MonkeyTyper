using Newtonsoft.Json.Serialization;
using System;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Forces to deserialize ISettings as an object.
    /// </summary>
    internal class SettingsContractResolver : DefaultContractResolver
    {
        /// <inheritdoc />
        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(ISettings).IsAssignableFrom(objectType))
                return base.CreateObjectContract(objectType);

            return base.CreateContract(objectType);
        }
    }
}
