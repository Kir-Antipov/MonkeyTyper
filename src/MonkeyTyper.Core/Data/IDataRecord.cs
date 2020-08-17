using System;
using System.Collections.Generic;

namespace MonkeyTyper.Core.Data
{
    /// <summary>
    /// Provides access to the property values within each record for a <see cref="IDataReader"/>, and is
    /// implemented by user data providers that can access different data sources.
    /// </summary>
    public interface IDataRecord : IEnumerable<object?>
    {
        /// <summary>
        /// Gets the property with the specified name.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The property with the specified name as an <see cref="object"/>.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        object? this[string name] { get; }

        /// <summary>
        /// Gets the property located at the specified index.
        /// </summary>
        /// <param name="i">The zero-based index of the property to get.</param>
        /// <returns>The property located at the specified index as an <see cref="object"/>.</returns>
        /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="PropertyCount"/>.</exception>
        object? this[int i] { get; }

        /// <summary>
        /// Gets the names of all properties in the current record.
        /// </summary>
        IReadOnlyList<string> PropertyNames { get; }

        /// <summary>
        /// Gets the number of property in the current record.
        /// </summary>
        int PropertyCount { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> information corresponding to the type of <see cref="object"/> that
        /// would be returned from <see cref="GetValue(string)"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>
        /// The <see cref="Type"/> information corresponding to the type of <see cref="object"/> that
        ///  would be returned from <see cref="GetValue(string)"/>.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        Type GetFieldType(string name);


        /// <summary>
        /// Gets the value of the specified property as a <see cref="bool"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        bool GetBoolean(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="byte"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        byte GetByte(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="byte[]"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        byte[] GetBytes(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="char"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        char GetChar(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="IDataReader"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        IDataReader? GetData(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        DateTime GetDateTime(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="decimal"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        decimal GetDecimal(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="double"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        double GetDouble(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="float"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        float GetFloat(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="Guid"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        Guid GetGuid(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="short"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        short GetInt16(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="int"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        int GetInt32(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="long"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        long GetInt64(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="string"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        /// <exception cref="InvalidCastException">The property value can't be cast to the specified type.</exception>
        string? GetString(string name);

        /// <summary>
        /// Gets the value of the specified property as a <see cref="object"/>.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="IndexOutOfRangeException">No property with the specified name was found.</exception>
        object? GetValue(string name);
    }
}
