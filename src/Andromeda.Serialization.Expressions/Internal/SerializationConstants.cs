using System;
using System.Buffers;

namespace Andromeda.Serialization.Expressions.Internal
{
    internal static class SerializationConstants
    {
        public static readonly Type ReadOnlySeqByRef = typeof(ReadOnlySequence<byte>).MakeByRefType();
        public static readonly Type SpanByRef = typeof(Span<byte>).MakeByRefType();
        public static readonly Type LongByRef = typeof(long).MakeByRefType();
        public static readonly Type Deserializer = typeof(IDeserializer);
        public static readonly Type Serializer = typeof(ISerializer);
    }
}
