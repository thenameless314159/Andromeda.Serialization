namespace Andromeda.Serialization
{
    // Utils to allow multiple SerializationStore within the same application context
    public abstract partial class SerializationType
    {
        public static readonly LittleEndian LittleEndian = new();
        public static readonly BigEndian BigEndian = new();
        public static readonly Other Other = new();
    }

    public class LittleEndian : SerializationType { }
    public class BigEndian : SerializationType { }
    public class Other : SerializationType { }
}
