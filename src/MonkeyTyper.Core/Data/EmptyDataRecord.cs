using System;
using System.Collections.Generic;

namespace MonkeyTyper.Core.Data
{
    /// <summary>
    /// Represents the empty record.
    /// </summary>
    internal sealed class EmptyDataRecord : DataRecord
    {
        /// <inheritdoc/>
        public override object? this[string name] => throw new IndexOutOfRangeException();
        
        /// <inheritdoc/>
        public override object? this[int i] => throw new IndexOutOfRangeException();

        /// <inheritdoc/>
        public override IReadOnlyList<string> PropertyNames => Array.Empty<string>();
        
        /// <inheritdoc/>
        public override int PropertyCount => 0;
    }
}
