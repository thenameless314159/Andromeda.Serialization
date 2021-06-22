using System;
using System.Threading.Tasks;

namespace Andromeda.Serialization
{
    internal sealed class SerializationBuilder<TEndianness> : SerializationBuilder
        where TEndianness : SerializationType, new()
    {
        public override SerializationType Endianness { get; } = new TEndianness();

        public override SerializationBuilder Configure<T>(DeserializerDlg<T> deserializeMethod) {
            SerializationStore<TEndianness>.Store<T>.Deserialize = deserializeMethod;
            return this;
        }

        public override SerializationBuilder Configure<T>(SerializerDlg<T> serializeMethod) {
            SerializationStore<TEndianness>.Store<T>.Serialize = serializeMethod;
            return this;
        }

        public override SerializationBuilder SetupStoreOf(params Type[] types)
        {
            SerializationStore<TEndianness>.Setup(MethodBuilder);
            Parallel.ForEach(types, SerializationStore<TEndianness>.SetupStoreOf);
            return this;
        }

        public override SerializationBuilder SetupStoreOf<T>()
        {
            SerializationStore<TEndianness>.Setup(MethodBuilder);
            SerializationStore<TEndianness>.SetupStore<T>();
            return this;
        }

        public override ISerDes Build() { SerializationStore<TEndianness>.Setup(MethodBuilder); 
            return new SerDes<TEndianness>(); }
    }
}
