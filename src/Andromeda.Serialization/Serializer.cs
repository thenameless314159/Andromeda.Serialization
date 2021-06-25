using System;

namespace Andromeda.Serialization
{
    internal sealed class Serializer<TEndianness> : ISerializer where TEndianness : SerializationType
    {
        public void Serialize<T>(in T value, in Span<byte> buffer, out int bytesWritten) => 
            SerializationStore<TEndianness>.Store<T>.Serialize(this, in buffer, value, out bytesWritten);
    }
}
