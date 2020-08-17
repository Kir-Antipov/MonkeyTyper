using System;
using System.Collections.Generic;

namespace MonkeyTyper.Core.Data
{
    /// <summary>
    /// Implements <see cref="DataRecord"/> based on <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    internal sealed class DictionaryDataRecord : DataRecord
    {
        /// <inheritdoc/>
        public override object? this[string name] => NamedValues.TryGetValue(name, out object? value) ? value : throw new IndexOutOfRangeException();

        /// <inheritdoc/>
        public override object? this[int i] => i >= 0 && i < Values.Count ? Values[i] : throw new IndexOutOfRangeException();

        /// <inheritdoc/>
        public override IReadOnlyList<string> PropertyNames => Names;

        /// <inheritdoc/>
        public override int PropertyCount => NamedValues.Count;

        private readonly Dictionary<string, object?> NamedValues;
        private readonly IReadOnlyList<string> Names;
        private readonly IReadOnlyList<object?> Values;

        /// <summary>
        /// Initialize a new instance of the <see cref="DictionaryDataRecord"/> class.
        /// </summary>
        /// <param name="source">
        /// The collection whose elements are copied to the new <see cref="DataReader"/>.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{T}"/> implementation to use when
        /// comparing keys.
        /// </param>
        public DictionaryDataRecord(IEnumerable<KeyValuePair<string, object?>> source, IEqualityComparer<string> comparer)
        {
            int capacity = source switch
            {
                ICollection<KeyValuePair<string, object?>> collection => collection.Count,
                IReadOnlyCollection<KeyValuePair<string, object?>> collection => collection.Count,
                _ => 4
            };

            NamedValues = new Dictionary<string, object?>(capacity, comparer);
            List<string> names = new List<string>(capacity);
            List<object?> values = new List<object?>(capacity);
            foreach (KeyValuePair<string, object?> pair in source)
            {
                NamedValues[pair.Key] = pair.Value;
                names.Add(pair.Key);
                values.Add(pair.Value);
            }
            Names = names.AsReadOnly();
            Values = values.AsReadOnly();
        }

        /// <inheritdoc cref="DictionaryDataRecord(IEnumerable{KeyValuePair{string, object}}, IEqualityComparer{string})"/>
        public DictionaryDataRecord(IEnumerable<(string Key, object? Value)> source, IEqualityComparer<string> comparer)
        {
            int capacity = source switch
            {
                ICollection<KeyValuePair<string, object?>> collection => collection.Count,
                IReadOnlyCollection<KeyValuePair<string, object?>> collection => collection.Count,
                _ => 4
            };

            NamedValues = new Dictionary<string, object?>(capacity, comparer);
            List<string> names = new List<string>(capacity);
            List<object?> values = new List<object?>(capacity);
            foreach (var (key, value) in source)
            {
                NamedValues[key] = value;
                names.Add(key);
                values.Add(value);
            }
            Names = names.AsReadOnly();
            Values = values.AsReadOnly();
        }

        /// <inheritdoc cref="DictionaryDataRecord(IEnumerable{KeyValuePair{string, object}}, IEqualityComparer{string})"/>
        public DictionaryDataRecord(IDictionary<string, object?> source, IEqualityComparer<string> comparer)
        {
            NamedValues = new Dictionary<string, object?>(source, comparer);
            List<string> names = new List<string>(NamedValues.Count);
            names.AddRange(NamedValues.Keys);
            Names = names.AsReadOnly();
            List<object?> values = new List<object?>(NamedValues.Count);
            values.AddRange(NamedValues.Values);
            Values = values.AsReadOnly();
        }
    }
}
