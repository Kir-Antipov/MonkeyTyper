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
    }
}
