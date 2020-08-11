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

        /// <inheritdoc cref="IDataRecord.this[string]"/>
        public abstract object? this[string name] { get; }

        /// <inheritdoc cref="IDataRecord.this[int]"/>
        public abstract object? this[int i] { get; }

        /// <inheritdoc cref="IDataRecord.PropertyNames"/>
        public abstract string[] PropertyNames { get; }

        /// <inheritdoc cref="IDataRecord.PropertyCount"/>
        public abstract int PropertyCount { get; }

        /// <inheritdoc cref="IDataRecord.GetFieldType(string)"/>
        public virtual Type GetFieldType(string name) => this[name]?.GetType() ?? typeof(object);


        /// <inheritdoc cref="IDataRecord.GetBoolean(string)"/>
        public virtual bool GetBoolean(string name) => GetValue<bool>(name);

        /// <inheritdoc cref="IDataRecord.GetByte(string)"/>
        public virtual byte GetByte(string name) => GetValue<byte>(name);

        /// <inheritdoc cref="IDataRecord.GetBytes(string)"/>
        public virtual byte[] GetBytes(string name) => GetString(name) is { } str ? Encoding.UTF8.GetBytes(str) : Array.Empty<byte>();

        /// <inheritdoc cref="IDataRecord.GetChar(string)"/>
        public virtual char GetChar(string name) => GetValue<char>(name);

        /// <inheritdoc cref="IDataRecord.GetData(string)"/>
        public virtual IDataReader? GetData(string name) => this[name] switch
        {
            IDataReader reader => reader,
            null => null,
            _ => throw new InvalidCastException()
        };

        /// <inheritdoc cref="IDataRecord.GetDateTime(string)"/>
        public virtual DateTime GetDateTime(string name) => GetValue<DateTime>(name);

        /// <inheritdoc cref="IDataRecord.GetDecimal(string)"/>
        public virtual decimal GetDecimal(string name) => GetValue<decimal>(name);

        /// <inheritdoc cref="IDataRecord.GetDouble(string)"/>
        public virtual double GetDouble(string name) => GetValue<double>(name);

        /// <inheritdoc cref="IDataRecord.GetFloat(string)"/>
        public virtual float GetFloat(string name) => GetValue<float>(name);

        /// <inheritdoc cref="IDataRecord.GetGuid(string)"/>
        public virtual Guid GetGuid(string name) => this[name] switch
        {
            Guid guid => guid,
            { } value => Guid.TryParse(value.ToString(), out Guid guid) ? guid : throw new InvalidCastException(),
            _ => default
        };

        /// <inheritdoc cref="IDataRecord.GetInt16(string)"/>
        public virtual short GetInt16(string name) => GetValue<short>(name);

        /// <inheritdoc cref="IDataRecord.GetInt32(string)"/>
        public virtual int GetInt32(string name) => GetValue<int>(name);

        /// <inheritdoc cref="IDataRecord.GetInt64(string)"/>
        public virtual long GetInt64(string name) => GetValue<long>(name);

        /// <inheritdoc cref="IDataRecord.GetString(string)"/>
        public virtual string? GetString(string name) => this[name] switch
        {
            string str => str,
            { } value => value.ToString(),
            _ => default
        };

        /// <inheritdoc cref="IDataRecord.GetValue(string)"/>
        public virtual object? GetValue(string name) => this[name];

        /// <inheritdoc cref="IEnumerable.GetEnumerator()"/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IEnumerable{object}.GetEnumerator()"/>
        public virtual IEnumerator<object?> GetEnumerator()
        {
            string[] names = PropertyNames;
            for (int i = 0; i < names.Length; ++i)
                yield return this[names[i]];
        }

        [return: MaybeNull]
        private T GetValue<T>(string name) => this[name] switch
        {
            T typed => typed,
            { } value => (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture),
            _ => default
        };
    }
}
