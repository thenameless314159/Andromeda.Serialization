using System;
using System.Buffers;

namespace Andromeda.Serialization
{
    public interface IDeserializer
    {
        bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out SequencePosition consumed, out SequencePosition examined) where T : new();
        bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out SequencePosition consumed, out SequencePosition examined);

        bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value) where T : new() => TryDeserialize(in buffer, out value, out _);
        bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value) => TryDeserialize(in buffer, value, out _);
        
        bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out long bytesRead) where T : new();
        bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out long bytesRead);

        T? Deserialize<T>(in ReadOnlySequence<byte> buffer, T value, out long bytesRead) => TryDeserialize(in buffer, value, out bytesRead) ? value : default;
        T? Deserialize<T>(in ReadOnlySequence<byte> buffer, T value) => Deserialize(in buffer, value, out _);

        T? Deserialize<T>(in ReadOnlySequence<byte> buffer, out long bytesRead) where T : new() => TryDeserialize(in buffer, out T? value, out bytesRead) ? value : default;
        T? Deserialize<T>(in ReadOnlySequence<byte> buffer) where T : new() => Deserialize<T>(in buffer, out _);
    }
}
