using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MonkeyTyper.Plugins.DataReaders.DSV
{
    /// <summary>
    /// DSV parser settings.
    /// </summary>
    [Guid("4DD21A83-790D-46D1-B484-B230CF4AECCC")]
    [DisplayName("DSV parser settings")]
    public sealed class DSVReaderSettings
    {
        /// <summary>
        /// Column separators.
        /// </summary>
        [DisplayName("Column separators (e.g., comma for CSV)")]
        public string Separators { get; set; } = string.Empty;

        /// <summary>
        /// File to parse.
        /// </summary>
        [DisplayName("File to parse")]
        public string Filename { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the <see cref="string.Trim()"/> method
        /// should be called on strings.
        /// </summary>
        [DisplayName("Trim whitespace")]
        public bool TrimEntries { get; set; } = true;
    }
}
