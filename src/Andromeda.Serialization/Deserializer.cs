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

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out SequencePosition consumed, 
            out SequencePosition examined) where T : new() 
        {
            value = new T();

            if (!SerializationStore<TEndianness>.Store<T>.Deserialize(this, in buffer, value, out var bytesRead))
            {
                var position = buffer.GetPosition(bytesRead);
                consumed = buffer.Start;
                examined = position;
                value = default;
                return false;
            }

            var pos = buffer.GetPosition(bytesRead);
            consumed = pos;
            examined = pos;
            return true;
        }

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out SequencePosition consumed,
            out SequencePosition examined)
        {
            if (!SerializationStore<TEndianness>.Store<T>.Deserialize(this, in buffer, value, out var bytesRead))
            {
                var position = buffer.GetPosition(bytesRead);
                consumed = buffer.Start;
                examined = position;
                return false;
            }

            var pos = buffer.GetPosition(bytesRead);
            consumed = pos;
            examined = pos;
            return true;
        }
    }
}
