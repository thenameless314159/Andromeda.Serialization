using System.Buffers;

namespace Andromeda.Serialization
{
    internal sealed class Deserializer<TEndianness> : IDeserializer where TEndianness : SerializationType
    {
        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out long bytesRead) =>
            SerializationStore<TEndianness>.Store<T>.Deserialize(this, in buffer, out value, out bytesRead);
    }
}
