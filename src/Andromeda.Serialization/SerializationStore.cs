using System;

namespace Andromeda.Serialization
{
    internal static class SerializationStore<TEndianness> where TEndianness : SerializationType
    {
        public static SerializationMethodBuilder? Builder { get; private set; }

        public static void Setup(SerializationMethodBuilder? builder) {
            if(Builder is not null) throw new InvalidOperationException(
                "SerializationMethodBuilder was already setup for the current " + nameof(SerializationStore<TEndianness>));

            Builder = builder;
        }

        public static class Store<T>
        {
            public static DeserializerWithPosDlg<T> DeserializeWithPos
            {
                get => _deserializerWithPos ??= CreateDeserializeWithPosMethod();
                set
                {
                    if (_desWithPosAlreadySet) throw new InvalidOperationException($"Another deserialization method with sequence position for {typeof(T).Name} has already been registered !");
                    _deserializerWithPos = value;
                    _desWithPosAlreadySet = true;
                }
            }

            public static DeserializerDlg<T> Deserialize
            {
                get => _deserializer ??= CreateDeserializeMethod();
                set
                {
                    if (_desAlreadySet) throw new InvalidOperationException($"Another deserialization method for {typeof(T).Name} has already been registered !");
                    _deserializer = value;
                    _desAlreadySet = true;
                }
            }

            public static SerializerDlg<T> Serialize
            {
                get => _serializer ??= CreateSerializeMethod();
                set
                {
                    
                    if (_serAlreadySet) throw new InvalidOperationException($"Another serialization method for {typeof(T).Name} has already been registered !");
                    _serAlreadySet = true;
                    _serializer = value;
                }
            }
            
            private static bool _desAlreadySet, _desWithPosAlreadySet, _serAlreadySet;
            private static DeserializerWithPosDlg<T>? _deserializerWithPos;
            private static DeserializerDlg<T>? _deserializer;
            private static SerializerDlg<T>? _serializer;

            public static DeserializerWithPosDlg<T> CreateDeserializeWithPosMethod() { _desAlreadySet = true; return Builder?.BuildDeserializeWithSeqPosition<T>() 
                ?? throw new InvalidOperationException("SerializationMethodBuilder is missing from the current " + nameof(SerializationStore<TEndianness>));
            }

            public static DeserializerDlg<T> CreateDeserializeMethod() { _desAlreadySet = true; return Builder?.BuildDeserialize<T>() 
                ?? throw new InvalidOperationException("SerializationMethodBuilder is missing from the current " + nameof(SerializationStore<TEndianness>));
            }

            public static SerializerDlg<T> CreateSerializeMethod() { _serAlreadySet = true; return Builder?.BuildSerialize<T>() 
                ?? throw new InvalidOperationException("SerializationMethodBuilder is missing from the current " + nameof(SerializationStore<TEndianness>));
            }

            // trigger get to build from method builder
            public static void Setup() {
                _ = DeserializeWithPos;
                _ = Deserialize;
                _ = Serialize;
            }
        }
    }
}
