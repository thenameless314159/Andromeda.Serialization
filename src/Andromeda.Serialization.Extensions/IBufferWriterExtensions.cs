using System.Diagnostics;
using Andromeda.Sizing;
using System.Buffers;

namespace Andromeda.Serialization
{
    public static class IBufferWriterExtensions
    {
        /// <summary>
        /// Try to serialize a value into a <see cref="IBufferWriter{T}"/> using an <see cref="ISizing"/>
        /// to get the size of the value.
        /// </summary>
        /// <typeparam name="T">The value to serialize.</typeparam>
        /// <param name="serializer">The serializer.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="writer">The buffer writer.</param>
        /// <param name="sizing">The sizing.</param>
        /// <returns>The number of bytes written in the writer.</returns>
        public static int Serialize<T>(this IBufferWriter<byte> writer, in T value, ISerializer serializer, ISizing sizing)
        {
            var sizeOf = sizing.SizeOf(value);
            var span = writer.GetSpan(sizeOf);

            serializer.Serialize(in value, in span, out var bytesWritten);
            Debug.Assert(bytesWritten == sizeOf, "ISizing.SizeOf<T>() didn't match with the number of bytes written by ISerializer",
                "sizeOf={sizeOf}, bytesWritten={bytesWritten}", sizeOf, bytesWritten);

            var cast = (int) bytesWritten;
            writer.Advance(cast);
            return cast;
        }
    }
}
