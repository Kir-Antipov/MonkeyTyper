using MonkeyTyper.Core.Data;
using System;

namespace MonkeyTyper.Core.Format
{
    /// <summary>
    /// Defines a method that supports custom string formatting.
    /// </summary>
    public interface IStringFormatter
    {
        /// <summary>
        /// Transforms record values to an equivalent string representation
        /// using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="record">A value provider.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>Formatted string.</returns>
        /// <exception cref="FormatException">Input string was not in a correct format.</exception>
        string Format(string format, IDataRecord record, IFormatProvider formatProvider);
    }
}
