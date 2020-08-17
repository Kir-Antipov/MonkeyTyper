using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace MonkeyTyper.Core.Data
{
    /// <summary>
    /// Base class for <see cref="IDataRecord"/>.
    /// </summary>
    public abstract class DataRecord : IDataRecord
    {
        /// <summary>
        /// Represents the empty record. This field is read-only.
        /// </summary>
        public static readonly DataRecord Empty = new EmptyDataRecord();

        /// <inheritdoc/>
        public abstract object? this[string name] { get; }

        /// <inheritdoc/>
        public abstract object? this[int i] { get; }

        /// <inheritdoc/>
        public abstract IReadOnlyList<string> PropertyNames { get; }

        /// <inheritdoc/>
        public abstract int PropertyCount { get; }

        /// <inheritdoc/>
        public virtual Type GetFieldType(string name) => this[name]?.GetType() ?? typeof(object);


        /// <inheritdoc/>
        public virtual bool GetBoolean(string name) => GetValue<bool>(name);

        /// <inheritdoc/>
        public virtual byte GetByte(string name) => GetValue<byte>(name);

        /// <inheritdoc/>
        public virtual byte[] GetBytes(string name) => GetString(name) is { } str ? Encoding.UTF8.GetBytes(str) : Array.Empty<byte>();

        /// <inheritdoc/>
        public virtual char GetChar(string name) => GetValue<char>(name);

        /// <inheritdoc/>
        public virtual IDataReader? GetData(string name) => this[name] switch
        {
            IDataReader reader => reader,
            null => null,
            _ => throw new InvalidCastException()
        };

        /// <inheritdoc/>
        public virtual DateTime GetDateTime(string name) => GetValue<DateTime>(name);

        /// <inheritdoc/>
        public virtual decimal GetDecimal(string name) => GetValue<decimal>(name);

        /// <inheritdoc/>
        public virtual double GetDouble(string name) => GetValue<double>(name);

        /// <inheritdoc/>
        public virtual float GetFloat(string name) => GetValue<float>(name);

        /// <inheritdoc/>
        public virtual Guid GetGuid(string name) => this[name] switch
        {
            Guid guid => guid,
            { } value => Guid.TryParse(value.ToString(), out Guid guid) ? guid : throw new InvalidCastException(),
            _ => default
        };

        /// <inheritdoc/>
        public virtual short GetInt16(string name) => GetValue<short>(name);

        /// <inheritdoc/>
        public virtual int GetInt32(string name) => GetValue<int>(name);

        /// <inheritdoc/>
        public virtual long GetInt64(string name) => GetValue<long>(name);

        /// <inheritdoc/>
        public virtual string? GetString(string name) => this[name] switch
        {
            string str => str,
            { } value => value.ToString(),
            _ => default
        };

        /// <inheritdoc/>
        public virtual object? GetValue(string name) => this[name];

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc/>
        public virtual IEnumerator<object?> GetEnumerator()
        {
            IReadOnlyList<string> names = PropertyNames;
            for (int i = 0; i < names.Count; ++i)
                yield return this[names[i]];
        }

        [return: MaybeNull]
        private T GetValue<T>(string name) => this[name] switch
        {
            T typed => typed,
            { } value => (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture),
            _ => default
        };

        #region Init
        /// <summary>
        /// Creates a <see cref="DataRecord"/> from key-value pairs.
        /// </summary>
        /// <param name="values">
        /// The values ​​to be accessed through the <see cref="DataRecord"/> interface.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{T}"/> implementation to use when
        /// comparing keys.
        /// </param>
        /// <returns>
        /// <see cref="DataRecord"/> providing access to the specified data.
        /// </returns>
        public static DataRecord From(IEnumerable<KeyValuePair<string, object?>> values, IEqualityComparer<string> comparer)
        {
            _ = values ?? throw new ArgumentNullException(nameof(values));
            _ = comparer ?? throw new ArgumentNullException(nameof(comparer));

            return new DictionaryDataRecord(values, comparer);
        }

        /// <inheritdoc cref="From(IEnumerable{KeyValuePair{string, object?}}, IEqualityComparer{string})"/>
        public static DataRecord From(IEnumerable<KeyValuePair<string, object?>> values) => From(values, StringComparer.InvariantCultureIgnoreCase);


        /// <inheritdoc cref="From(IEnumerable{KeyValuePair{string, object?}}, IEqualityComparer{string})"/>
        public static DataRecord From(IEqualityComparer<string> comparer, params (string, object?)[] values) => From(values, comparer);

        /// <inheritdoc cref="From(IEnumerable{KeyValuePair{string, object?}}, IEqualityComparer{string})"/>
        public static DataRecord From(params (string, object?)[] values) => From(values, StringComparer.InvariantCultureIgnoreCase);

        /// <inheritdoc cref="From(IEnumerable{KeyValuePair{string, object?}}, IEqualityComparer{string})"/>
        public static DataRecord From(IEnumerable<(string, object?)> values) => From(values, StringComparer.InvariantCultureIgnoreCase);

        /// <inheritdoc cref="From(IEnumerable{KeyValuePair{string, object?}}, IEqualityComparer{string})"/>
        public static DataRecord From(IEnumerable<(string, object?)> values, IEqualityComparer<string> comparer)
        {
            _ = values ?? throw new ArgumentNullException(nameof(values));
            _ = comparer ?? throw new ArgumentNullException(nameof(comparer));

            return new DictionaryDataRecord(values, comparer);
        }


        /// <inheritdoc cref="From(IEnumerable{KeyValuePair{string, object?}}, IEqualityComparer{string})"/>
        public static DataRecord From(IDictionary<string, object?> values) => From(values, StringComparer.InvariantCultureIgnoreCase);

        /// <inheritdoc cref="From(IEnumerable{KeyValuePair{string, object?}}, IEqualityComparer{string})"/>
        public static DataRecord From(IDictionary<string, object?> values, IEqualityComparer<string> comparer)
        {
            _ = values ?? throw new ArgumentNullException(nameof(values));
            _ = comparer ?? throw new ArgumentNullException(nameof(comparer));

            return new DictionaryDataRecord(values, comparer);
        }
        #endregion
    }
}
