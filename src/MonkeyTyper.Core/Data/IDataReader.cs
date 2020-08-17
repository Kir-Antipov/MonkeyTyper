using System;
using System.Threading.Tasks;

namespace MonkeyTyper.Core.Data
{
    /// <summary>
    /// Provides a means of reading one or more forward-only streams of result sets obtained
    /// from a data source, and is implemented by user data providers that can
    /// access different data sources.
    /// </summary>
    public interface IDataReader : IDataRecord, IAsyncDisposable, IDisposable
    {
        /// <summary>
        /// Gets the total number of records.
        /// </summary>
        /// <remarks>
        /// If the number of records isn't known,
        /// this property will return -1.
        /// </remarks>
        int Count { get; }

        /// <summary>
        /// Advances the <see cref="IDataReader"/> to the next record.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if there are more rows; otherwise, <see langword="false"/>.
        /// </returns>
        bool Read();
        /// <inheritdoc cref="Read"/>
        Task<bool> ReadAsync();

        /// <summary>
        /// Advances the <see cref="IDataReader"/> to the next result set.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if there are more rows; otherwise, <see langword="false"/>.
        /// </returns>
        bool NextResult();
        /// <inheritdoc cref="NextResult"/>
        Task<bool> NextResultAsync();

        /// <summary>
        /// Sets the <see cref="IDataReader"/> to its initial position,
        /// which is before the first element in the data source.
        /// </summary>
        void Reset();
        /// <inheritdoc cref="Reset"/>
        /// <returns>An asynchronous task context.</returns>
        Task ResetAsync();
    }
}
