using MonkeyTyper.Core.Data;
using System;
using System.Text.RegularExpressions;

namespace MonkeyTyper.Core.Format
{
    /// <summary>
    /// Default implementation of the <see cref="IStringFormatter"/>.
    /// </summary>
    public class DefaultStringFormatter : StringFormatter
    {
        private static Regex FormatExtractor { get; } = new Regex(@"(?<!(?:(?:^|[^\\])(?:[\\]{2})*[\\])){(?<expression>[^}:]+)(?::(?<format>[^}]+))?}", RegexOptions.Compiled | RegexOptions.Multiline);
        private static Regex SlashExtractor { get; } = new Regex(@"[\\]+", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <inheritdoc cref="StringFormatter.Format(string, IDataRecord, IFormatProvider)"/>
        public sealed override string Format(string format, IDataRecord record, IFormatProvider formatProvider)
        {
            _ = format ?? throw new ArgumentNullException(nameof(format));
            _ = record ?? throw new ArgumentNullException(nameof(record));
            _ = formatProvider ?? throw new ArgumentNullException(nameof(formatProvider));

            string formatted;
            try
            {
                formatted = FormatExtractor.Replace(format, x => Format(x.Groups["expression"].Value, x.Groups["format"].Value, record, formatProvider));
            }
            catch (Exception e)
            {
                throw new FormatException("Input string was not in a correct format.", e);
            }

            return SlashExtractor.Replace(formatted, x => new string('\\', x.Length / 2));
        }

        /// <summary>
        /// Transforms expression to an equivalent string representation
        /// using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="expression">An expression.</param>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="record">A value provider.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>
        /// The string representation of the value of expression,
        /// formatted as specified by format and formatProvider.
        /// </returns>
        protected virtual string Format(string expression, string? format, IDataRecord record, IFormatProvider formatProvider)
        {
            object? value = record.GetValue(expression);
            if (value is null)
                return string.Empty;

            if (!string.IsNullOrEmpty(format) && value is IFormattable formattable)
                return formattable.ToString(format, formatProvider);

            return value.ToString();
        }
    }
}
