using System;
using System.Buffers;
using Andromeda.Sizing;

namespace Andromeda.Serialization.Expressions.Internal
{
    internal static class CommonTypes
    {
        public static readonly Type ReadOnlySeqByRef = typeof(ReadOnlySequence<byte>).MakeByRefType();
        public static readonly Type SpanByRef = typeof(Span<byte>).MakeByRefType();
        public static readonly Type LongByRef = typeof(long).MakeByRefType();
        public static readonly Type Deserializer = typeof(IDeserializer);
        public static readonly Type SizingInterface = typeof(ISizing);
        public static readonly Type Serializer = typeof(ISerializer);
    }
}
