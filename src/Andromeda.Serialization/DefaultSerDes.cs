using System;
using System.Buffers;

namespace Andromeda.Serialization
{
    public class DefaultSerDes : ISerDes
    {
        public DefaultSerDes(IDeserializer des, ISerializer ser) => (_des, _ser) = (des, ser);
        private readonly IDeserializer _des; private readonly ISerializer _ser;

        public void Serialize<T>(in T value, in Span<byte> buffer) => _ser.Serialize(in value, in buffer);

        public void Serialize<T>(in T value, in Span<byte> buffer, out int bytesWritten) =>
            _ser.Serialize(in value, in buffer, out bytesWritten);

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out SequencePosition consumed, 
            out SequencePosition examined) where T : new() => _des.TryDeserialize(in buffer, out value, out consumed, out examined);

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out SequencePosition consumed,
            out SequencePosition examined) => _des.TryDeserialize(in buffer, value, out consumed, out examined);

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, out T? value, out long bytesRead)
            where T : new() => _des.TryDeserialize(in buffer, out value, out bytesRead);

        public bool TryDeserialize<T>(in ReadOnlySequence<byte> buffer, T value, out long bytesRead) =>
            _des.TryDeserialize(in buffer, value, out bytesRead);
    }
}
