using MonkeyTyper.Core.Data;
using System;

namespace MonkeyTyper.Core.Format
{
    /// <summary>
    /// Base class for <see cref="IStringFormatter"/>.
    /// </summary>
    public abstract class StringFormatter : IStringFormatter
    {
        /// <inheritdoc cref="IStringFormatter.Format(string, IDataRecord, IFormatProvider)"/>
        public abstract string Format(string format, IDataRecord record, IFormatProvider formatProvider);
    }
}
