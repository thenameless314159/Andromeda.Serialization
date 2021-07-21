using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Andromeda.Serialization
{
    public static class IDeserializerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryDeserialize<T>(this IDeserializer des, in ReadOnlySequence<byte> buffer, out T? value, out SequencePosition consumed,
            out SequencePosition examined)
        {
            if (!des.TryDeserialize(in buffer, out value, out var bytesRead))
            {
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Deserialize<T>(this IDeserializer des, in ReadOnlySequence<byte> buffer,
            out SequencePosition consumed, out SequencePosition examined)
        {
            des.TryDeserialize<T>(in buffer, out var value, out consumed, out examined);
            return value;
        }
        
    }
}
