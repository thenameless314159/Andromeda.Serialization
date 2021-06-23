using System;
using System.Buffers;

namespace Andromeda.Serialization
{
    internal sealed class Deserializer<TEndianness> : IDeserializer where TEndianness : SerializationType
    {
        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T value, out long bytesRead) where T : new() {
            value = new T(); return SerializationStore<TEndianness>.Store<T>.Deserialize(this, in buffer, value, out bytesRead);
        }

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out long bytesRead) => SerializationStore<TEndianness>
            .Store<T>.Deserialize(this, in buffer, value, out bytesRead);

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out SequencePosition consumed, 
            out SequencePosition examined) where T : new() 
        {
            value = new T();

            // TODO: Use GetPosition instead of slicing ?
            if (!SerializationStore<TEndianness>.Store<T>.Deserialize(this, in buffer, value, out var bytesRead)) {
                var remaining = buffer.Slice(bytesRead);
                examined = remaining.Start;
                consumed = buffer.Start;
                value = default;
                return false;
            }

            var rem = buffer.Slice(bytesRead);
            consumed = rem.Start;
            examined = rem.Start;
            return true;
        }

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out SequencePosition consumed,
            out SequencePosition examined)
        {
            // TODO: Use GetPosition instead of slicing ?
            if (!SerializationStore<TEndianness>.Store<T>.Deserialize(this, in buffer, value, out var bytesRead)) {
                var remaining = buffer.Slice(bytesRead);
                examined = remaining.Start;
                consumed = buffer.Start;
                return false;
            }

            var rem = buffer.Slice(bytesRead);
            consumed = rem.Start;
            examined = rem.Start;
            return true;
        }
    }
}
