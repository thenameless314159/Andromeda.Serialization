using System;
using System.Reflection;

namespace Andromeda.Serialization
{
    internal sealed class SerializationBuilder<TEndianness> : SerializationBuilder
        where TEndianness : SerializationType
    {
        public override SerializationBuilder Configure<T>(DeserializerWithPosDlg<T> deserializeMethod) {
            SerializationStore<TEndianness>.Store<T>.DeserializeWithPos = deserializeMethod;
            return this;
        }

        public override SerializationBuilder Configure<T>(DeserializerDlg<T> deserializeMethod) {
            SerializationStore<TEndianness>.Store<T>.Deserialize = deserializeMethod;
            return this;
        }

        public override SerializationBuilder Configure<T>(SerializerDlg<T> serializeMethod) {
            SerializationStore<TEndianness>.Store<T>.Serialize = serializeMethod;
            return this;
        }

        private static void SetupStore<T>() => SerializationStore<TEndianness>.Store<T>.Setup();
        public override SerializationBuilder SetupStoreOf(params Type[] types)
        {
            SerializationStore<TEndianness>.Setup(MethodBuilder);
            var setupMi = typeof(SerializationBuilder<TEndianness>).GetMethod(
                nameof(SetupStore), BindingFlags.NonPublic)!;

            foreach (var type in types) setupMi
                .MakeGenericMethod(type)
                .Invoke(this, Array.Empty<object>());

            return this;
        }

        public override SerializationBuilder SetupStoreOf<T>()
        {
            SerializationStore<TEndianness>.Setup(MethodBuilder);
            SerializationStore<TEndianness>.Store<T>.Setup();
            return this;
        }

        public override ISerDes Build() { SerializationStore<TEndianness>.Setup(MethodBuilder); 
            return new SerDes<TEndianness>(); }
    }
}
