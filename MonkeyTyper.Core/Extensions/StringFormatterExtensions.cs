using MonkeyTyper.Core.Data;
using MonkeyTyper.Core.Format;
using System;
using System.Globalization;

namespace MonkeyTyper.Core.Extensions
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) helpful methods for objects
    /// that implement <see cref="IStringFormatter"/>.
    /// </summary>
    public static class StringFormatterExtensions
    {
        /// <param name="formatter"><see cref="IStringFormatter"/> instance.</param>
        /// <inheritdoc cref="IStringFormatter.Format(string, IDataRecord, IFormatProvider)"/>
        public static string Format(this IStringFormatter formatter, string format, IDataRecord record)
        {
            _ = formatter ?? throw new ArgumentNullException(nameof(formatter));
            _ = format ?? throw new ArgumentNullException(nameof(format));
            _ = record ?? throw new ArgumentNullException(nameof(record));

            return formatter.Format(format, record, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc cref="Format(IStringFormatter, string, IDataRecord)"/>
        public static string Format(this IStringFormatter formatter, string format) => Format(formatter, format, DataRecord.Empty);
    }
}
