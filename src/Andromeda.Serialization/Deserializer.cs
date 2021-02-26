using System;
using System.Buffers;

namespace Andromeda.Serialization
{
    internal sealed class Deserializer<TEndianness> : IDeserializer where TEndianness : SerializationType
    {
        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T value, out long bytesRead) where T : new() {
            value = new T(); return SerializationStore<TEndianness>.Store<T>.Deserialize(this, in buffer, value, out bytesRead);
        }

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out long bytesRead) => SerializationStore<LittleEndian>
            .Store<T>.Deserialize(this, in buffer, value, out bytesRead);

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out SequencePosition consumed, out SequencePosition examined) where T : new() {
            value = new T(); return SerializationStore<TEndianness>.Store<T>.DeserializeWithPos(this, in buffer, value, out consumed, out examined);
        }

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out SequencePosition consumed, out SequencePosition examined) => 
            SerializationStore<TEndianness>.Store<T>.DeserializeWithPos(this, in buffer, value, out consumed, out examined);
    }
}
