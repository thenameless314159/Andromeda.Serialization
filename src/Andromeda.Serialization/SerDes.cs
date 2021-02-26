namespace Andromeda.Serialization
{
    internal sealed class SerDes<TEndianness> : DefaultSerDes where TEndianness : SerializationType
    { 
        public SerDes() : base(new Deserializer<TEndianness>(), new Serializer<TEndianness>()) { }
    }
}
