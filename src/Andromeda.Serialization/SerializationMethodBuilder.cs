using System;
using System.Buffers;

namespace Andromeda.Serialization
{
    public delegate bool DeserializerWithPosDlg<in T>(IDeserializer ser, in ReadOnlySequence<byte> seq, T value, out SequencePosition consumed, out SequencePosition examined);
    public delegate bool DeserializerDlg<in T>(IDeserializer ser, in ReadOnlySequence<byte> seq, T value, out long bytesRead);
    public delegate void SerializerDlg<T>(ISerializer ser, ref Span<byte> buf, in T value, out long bytesWritten);

    public abstract class SerializationMethodBuilder
    { 
        public abstract DeserializerWithPosDlg<T> BuildDeserializeWithSeqPosition<T>();
        public abstract DeserializerDlg<T> BuildDeserialize<T>();
        public abstract SerializerDlg<T> BuildSerialize<T>();
    }
}
