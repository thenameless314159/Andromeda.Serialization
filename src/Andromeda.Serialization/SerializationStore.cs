using System;
using System.Reflection;

namespace Andromeda.Serialization
{
    internal static class SerializationStore<TEndianness> where TEndianness : SerializationType
    {
        public static SerializationMethodBuilder? Builder { get; private set; }

        public static void Setup(SerializationMethodBuilder? builder) {
            if(Builder is not null) return;
            Builder = builder;
        }

        public static void SetupStore<T>() => Store<T>.Setup();
        public static void SetupStoreOf(Type type) => _setupMi.MakeGenericMethod(type).Invoke(null, Array.Empty<object>());
        private static readonly MethodInfo _setupMi = typeof(SerializationStore<TEndianness>).GetMethod(nameof(SetupStore))!;

        public static class Store<T>
        {
            public static DeserializerDlg<T> Deserialize
            {
                get => _deserializer ??= CreateDeserializeMethod();
                set {
                    if (_desAlreadySet) throw new InvalidOperationException(
                        $"Another deserialization method for {typeof(T).Name} has already been registered !");

                    _deserializer = value;
                    _desAlreadySet = true;
                }
            }

            public static SerializerDlg<T> Serialize
            {
                get => _serializer ??= CreateSerializeMethod();
                set {
                    if (_serAlreadySet) throw new InvalidOperationException(
                        $"Another serialization method for {typeof(T).Name} has already been registered !");

                    _serializer = value;
                    _serAlreadySet = true;
                }
            }
            
            private static bool _desAlreadySet, _serAlreadySet;
            private static DeserializerDlg<T>? _deserializer;
            private static SerializerDlg<T>? _serializer;

            // TODO: set after try build
            public static DeserializerDlg<T> CreateDeserializeMethod() { _desAlreadySet = true; return Builder?.BuildDeserialize<T>() 
                ?? throw new InvalidOperationException("SerializationMethodBuilder is missing from the current " + nameof(SerializationStore<TEndianness>) + "of " + typeof(TEndianness).Name);
            }

            // TODO: set after try build
            public static SerializerDlg<T> CreateSerializeMethod() { _serAlreadySet = true; return Builder?.BuildSerialize<T>() 
                ?? throw new InvalidOperationException("SerializationMethodBuilder is missing from the current " + nameof(SerializationStore<TEndianness>) + "of " + typeof(TEndianness).Name);
            }

            // trigger get to build from method builder
            public static void Setup() { _ = Deserialize; _ = Serialize; }
        }
    }
}
