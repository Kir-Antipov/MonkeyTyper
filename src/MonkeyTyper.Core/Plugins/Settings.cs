using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MonkeyTyper.Core.Plugins
{
    /// <summary>
    /// Base class for <see cref="ISettings"/>.
    /// </summary>
    /// <remarks>
    /// This class uses public read/write properties
    /// of the inherited type as available settings.
    /// <para></para>
    /// Don't forget to mark the inherited type
    /// with the <see cref="GuidAttribute"/>.
    /// </remarks>
    public abstract class Settings : ISettings
    {
        #region Var
        private static Regex NameExtractor { get; } = new Regex(@"Settings?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private Dictionary<string, PropertyInfo> Properties { get; }

        /// <inheritdoc cref="ISettings.DisplayName"/>
        public virtual string DisplayName { get; }

        /// <inheritdoc cref="ISettings.Guid"/>
        public virtual Guid Guid { get; }

        /// <inheritdoc cref="IDictionary{string, object}.Keys"/>
        public ICollection<string> Keys { get; }

        /// <inheritdoc cref="IDictionary{string, object}.Values"/>
        public ICollection<object?> Values => Properties.Values.Select(x => x.GetValue(this)).ToArray();

        /// <inheritdoc cref="ICollection{KeyValuePair{string, object}}.IsReadOnly"/>
        public bool IsReadOnly => false;

        /// <inheritdoc cref="ICollection{KeyValuePair{string, object}}.Count"/>
        public int Count => Properties.Count;
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            Type thisType = GetType();

            DisplayName = thisType.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ??
                NameExtractor.Replace(thisType.Name, x => string.Empty);

            Guid = thisType.GUID;

            Properties = thisType
                .GetProperties()
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length == 0)
                .ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);

            Keys = Properties.Keys.ToList().AsReadOnly();
        }
        #endregion

        #region Functions
        /// <inheritdoc/>
        public string GetDisplayName(string propertyName)
        {
            if (Properties.TryGetValue(propertyName, out PropertyInfo property))
                return property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name;

            throw new KeyNotFoundException();
        }

        /// <inheritdoc cref="IDictionary{string, object}.this[string]"/>
        public object? this[string key]
        {
            get => TryGetValue(key, out object? value) ? value : throw new KeyNotFoundException();
            set => Add(key, value);
        }

        /// <inheritdoc cref="IDictionary{string, object}.Add(string, object)"/>
        public void Add(string key, object? value)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            if (Properties.TryGetValue(key, out PropertyInfo? property))
                property.SetValue(this, ChangeType(value, property.PropertyType));
            else
                throw new NotSupportedException();
        }

        /// <inheritdoc cref="IDictionary{string, object}.ContainsKey(string)"/>
        public bool ContainsKey(string key) => Properties.ContainsKey(key ?? throw new ArgumentNullException(nameof(key)));

        /// <inheritdoc cref="IDictionary{string, object}.TryGetValue(string, out object)"/>
        public bool TryGetValue(string key, out object? value)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            if (Properties.TryGetValue(key, out PropertyInfo? property))
            {
                value = property.GetValue(this);
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        /// <inheritdoc cref="IDictionary{string, object}.Remove(string)"/>
        public bool Remove(string key) => throw new NotSupportedException();


        /// <inheritdoc cref="ICollection{KeyValuePair{string, object}}.Add(KeyValuePair{string, object})"/>
        void ICollection<KeyValuePair<string, object?>>.Add(KeyValuePair<string, object?> item) => Add(item.Key, item.Value);

        /// <inheritdoc cref="ICollection{KeyValuePair{string, object}}.Contains(KeyValuePair{string, object})"/>
        bool ICollection<KeyValuePair<string, object?>>.Contains(KeyValuePair<string, object?> item) => TryGetValue(item.Key, out object? value) && Equals(item.Value, value);
        
        /// <inheritdoc cref="ICollection{KeyValuePair{string, object}}.Remove(KeyValuePair{string, object})"/>
        bool ICollection<KeyValuePair<string, object?>>.Remove(KeyValuePair<string, object?> item) => throw new NotSupportedException();
        
        /// <inheritdoc cref="ICollection{KeyValuePair{string, object}}.Clear"/>
        void ICollection<KeyValuePair<string, object?>>.Clear() => throw new NotSupportedException();
        
        /// <inheritdoc cref="ICollection{KeyValuePair{string, object}}.CopyTo(KeyValuePair{string, object}[], int)"/>
        void ICollection<KeyValuePair<string, object?>>.CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
        {
            foreach (var pair in this)
            {
                if (arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException(nameof(arrayIndex));

                array[arrayIndex++] = pair;
            }
        }


        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IEnumerable{KeyValuePair{string, object}}.GetEnumerator"/>
        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator() =>
            Properties.Select(x => new KeyValuePair<string, object?>(x.Key, x.Value.GetValue(this))).GetEnumerator();

        /// <inheritdoc cref="Convert.ChangeType(object, Type)"/>
        private static object? ChangeType(object? value, Type conversionType)
        {
            if (value is null)
                return conversionType.IsValueType ? 
                    Activator.CreateInstance(conversionType) :
                    null;

            Type type = value.GetType();
            if (conversionType.IsAssignableFrom(type))
                return value;

            Type? underlyingType = Nullable.GetUnderlyingType(conversionType);
            object? converted = null;
            try
            {
                converted = Convert.ChangeType(value, underlyingType ?? conversionType);
            }
            catch { }

            if (converted is null && type == typeof(string) && (underlyingType ?? conversionType).GetMethod("Parse", new[] { typeof(string), typeof(IFormatProvider) }) is { IsStatic: true } parser)
                converted = parser.Invoke(null, new[] { value, CultureInfo.InvariantCulture });

            if (converted is null)
                throw new InvalidCastException();

            if (underlyingType is { })
                converted = conversionType.GetConstructors()[0].Invoke(new[] { converted });

            return converted;
        }
        #endregion
    }
}
