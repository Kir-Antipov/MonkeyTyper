using MonkeyTyper.Core.Data;
using System;

namespace MonkeyTyper.Core.Extensions
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) helpful methods for objects
    /// that implement <see cref="IDataRecord"/>.
    /// </summary>
    public static class DataRecordExtensions
    {
        /// <summary>
        /// Gets the name for the property to find.
        /// </summary>
        /// <param name="record"><see cref="IDataRecord"/> instance.</param>
        /// <param name="i">The index of the property to find.</param>
        /// <returns>The name of the property or the <see cref="string.Empty"/>, if there is no value to return.</returns>
        /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.PropertyCount"/>.</exception>
        public static string GetName(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.PropertyNames[i];
        }

        /// <summary>
        /// Return the index of the named field.
        /// </summary>
        /// <param name="record"><see cref="IDataRecord"/> instance.</param>
        /// <param name="name">The name of the property to find.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the comparison.</param>
        /// <returns>The index of the named property or -1, if there is no value to return.</returns>
        public static int GetOrdinal(this IDataRecord record, string name, StringComparison comparisonType)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            _ = name ?? throw new ArgumentNullException(nameof(name));

            for (int i = 0; i < record.PropertyNames.Count; ++i)
                if (string.Equals(record.PropertyNames[i], name, comparisonType))
                    return i;
            return -1;
        }

        /// <inheritdoc cref="GetOrdinal(IDataRecord, string, StringComparison)"/>
        public static int GetOrdinal(this IDataRecord record, string name) => GetOrdinal(record, name, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Gets the <see cref="Type"/> information corresponding to the type of <see cref="object"/> that
        /// would be returned from <see cref="GetValue(string)"/>.
        /// </summary>
        /// <param name="record"><see cref="IDataRecord"/> instance.</param>
        /// <param name="i">The zero-based property ordinal.</param>
        /// <returns>
        /// The <see cref="Type"/> information corresponding to the type of <see cref="object"/> that
        ///  would be returned from <see cref="GetValue(string)"/>.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        public static Type GetFieldType(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetFieldType(record.PropertyNames[i]);
        }


        /// <summary>
        /// Gets the value of the specified property as a <see cref="bool"/>.
        /// </summary>
        /// <param name="record"><see cref="IDataRecord"/> instance.</param>
        /// <param name="i">The zero-based property ordinal.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="PropertyCount"/>.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        public static bool GetBoolean(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetBoolean(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="byte"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static byte GetByte(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetByte(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="byte[]"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static byte[] GetBytes(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetBytes(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="char"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static char GetChar(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetChar(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="IDataReader"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static IDataReader? GetData(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetData(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="DateTime"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static DateTime GetDateTime(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetDateTime(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="decimal"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static decimal GetDecimal(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetDecimal(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="double"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static double GetDouble(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetDouble(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="float"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static float GetFloat(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetFloat(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="Guid"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static Guid GetGuid(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetGuid(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="short"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static short GetInt16(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetInt16(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="int"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static int GetInt32(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetInt32(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="long"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static long GetInt64(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetInt64(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="string"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static string? GetString(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetString(record.PropertyNames[i]);
        }

        /// <summary>
        /// Gets the value of the specified property as a <see cref="object"/>.
        /// </summary>
        /// <inheritdoc cref="GetBoolean(IDataRecord, int)"/>
        public static object? GetValue(this IDataRecord record, int i)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record));
            if (i < 0 || i > record.PropertyCount)
                throw new IndexOutOfRangeException(nameof(i));

            return record.GetValue(record.PropertyNames[i]);
        }
    }
}
