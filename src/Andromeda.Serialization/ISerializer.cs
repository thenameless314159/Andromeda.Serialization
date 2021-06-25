using System;

namespace Andromeda.Serialization
{
    public interface ISerializer
    {
        void Serialize<T>(in T value, in Span<byte> buffer) => Serialize(in value, in buffer, out _);
        void Serialize<T>(in T value, in Span<byte> buffer, out int bytesWritten);
    }
}
