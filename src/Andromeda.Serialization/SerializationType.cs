namespace Andromeda.Serialization
{
    // Utils to allow multiple SerializationStore within the same application context
    public abstract partial class SerializationType
    {
        public static readonly LittleEndian LittleEndian = new();
        public static readonly BigEndian BigEndian = new();
        public static readonly Other Other = new();
    }

    public sealed class LittleEndian : SerializationType { }
    public sealed class BigEndian : SerializationType { }
    public sealed class Other : SerializationType { }
}
