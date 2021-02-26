using System;

namespace Andromeda.Serialization
{
    internal sealed class Serializer<TEndianness> : ISerializer where TEndianness : SerializationType
    {
        public void Serialize<T>(in T value, ref Span<byte> buffer, out long bytesWritten) => 
            SerializationStore<TEndianness>.Store<T>.Serialize(this, ref buffer, value, out bytesWritten);
    }
}
