using System;
using System.Threading.Tasks;

namespace MonkeyTyper.Core.Data
{
    /// <summary>
    /// Base class for <see cref="IDataReader"/>.
    /// </summary>
    public abstract class DataReader : DataRecord, IDataReader
    {
        /// <inheritdoc/>
        public virtual int Count => -1;

        /// <inheritdoc/>
        public virtual bool Read() => ReadAsync().Result;

        /// <inheritdoc/>
        public virtual Task<bool> ReadAsync() => Task.FromResult(Read());


        /// <inheritdoc/>
        public virtual bool NextResult() => NextResultAsync().Result;

        /// <inheritdoc/>
        public virtual Task<bool> NextResultAsync() => Task.FromResult(NextResult());


        /// <inheritdoc/>
        public virtual void Reset() => ResetAsync().Wait();

        /// <inheritdoc/>
        public virtual Task ResetAsync()
        {
            Reset();
            return Task.CompletedTask;
        }


        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <param name="disposing">
        /// Indicates whether this method was called from 
        /// managed code (<see langword="true"/>)
        /// or from the finalizer (<see langword="false"/>).
        /// </param>
        /// <inheritdoc cref="IDisposable.Dispose"/>
        protected virtual void Dispose(bool disposing) => DisposeAsync().AsTask().Wait();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous dispose operation.
        /// </returns>
        public virtual ValueTask DisposeAsync()
        {
            Dispose(true);
            return default;
        }
    }
}
