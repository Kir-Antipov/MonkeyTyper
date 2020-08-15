using System;
using System.Collections.Generic;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Describes the available settings for the registered service.
    /// </summary>
    public interface ISettings : IDictionary<string, object?>
    {
        /// <summary>
        /// Human readable name of the settings section.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Unique and permanent identifier of this type.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// Gets the display name for the property with the specified name.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <returns>Display name of the property.</returns>
        string GetDisplayName(string propertyName);
    }
}
