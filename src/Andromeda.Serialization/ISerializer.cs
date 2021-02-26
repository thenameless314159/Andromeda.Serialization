using System;

namespace Andromeda.Serialization
{
    public interface ISerializer
    {
        void Serialize<T>(in T value, ref Span<byte> buffer) => Serialize(in value, ref buffer, out _);
        void Serialize<T>(in T value, ref Span<byte> buffer, out long bytesWritten);
    }
}
