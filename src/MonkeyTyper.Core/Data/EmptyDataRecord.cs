using System;

namespace MonkeyTyper.Core.Data
{
    internal class EmptyDataRecord : DataRecord
    {
        public override object? this[string name] => throw new IndexOutOfRangeException();
        public override object? this[int i] => throw new IndexOutOfRangeException();

        public override string[] PropertyNames => Array.Empty<string>();
        public override int PropertyCount => 0;
    }
}
