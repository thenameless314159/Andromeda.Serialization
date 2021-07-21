using System;
using System.Buffers;

namespace Andromeda.Serialization
{
    public delegate bool DeserializerDlg<T>(IDeserializer ser, in ReadOnlySequence<byte> seq, out T? value, out long bytesRead);
    public delegate void SerializerDlg<in T>(ISerializer ser, in Span<byte> buf, T value, out int bytesWritten);

    public abstract class SerializationMethodBuilder
    {
        public abstract DeserializerDlg<T> BuildDeserialize<T>();
        public abstract SerializerDlg<T> BuildSerialize<T>();
    }
}
