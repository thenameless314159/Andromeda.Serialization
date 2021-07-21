using System.Buffers;

namespace Andromeda.Serialization
{
    public interface IDeserializer
    {
        bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out long bytesRead);

        T? Deserialize<T>(in ReadOnlySequence<byte> buffer, out long bytesRead) => 
            TryDeserialize(in buffer, out T? value, out bytesRead) ? value : default;
    }
}
