using System;
using System.Reflection;

namespace Andromeda.Serialization
{
    public abstract class SerializationBuilder
    {
        private static readonly MethodInfo _createForMi = typeof(SerializationBuilder).GetMethod(nameof(Create), Type.EmptyTypes)!;
        private static SerializationBuilder CreateFrom(SerializationType type) => (SerializationBuilder)
            _createForMi.MakeGenericMethod(type.GetType()).Invoke(null, Array.Empty<object>())!;

        public static SerializationBuilder CreateDefault() => BitConverter.IsLittleEndian 
            ? new SerializationBuilder<LittleEndian>()
            : new SerializationBuilder<BigEndian>();

        public static SerializationBuilder Create(SerializationType type) => type switch {
            LittleEndian => CreateFor<LittleEndian>(),
            BigEndian => CreateFor<BigEndian>(),
            Other => CreateFor<Other>(),
            _ => CreateFrom(type)
        };

        public static SerializationBuilder CreateFor<T>() where T : SerializationType =>
            new SerializationBuilder<T>();

        protected SerializationBuilder(SerializationMethodBuilder? methodBuilder = default) =>
            MethodBuilder = methodBuilder;

        public SerializationMethodBuilder? MethodBuilder { get; set; }

        public SerializationBuilder Use(SerializationMethodBuilder builder)
        {
            MethodBuilder = builder;
            return this;
        }

        public SerializationBuilder Use<T>(T methodBuilder) where T : SerializationMethodBuilder
        {
            MethodBuilder = methodBuilder;
            return this;
        }

        public  SerializationBuilder Use<T>() where T : SerializationMethodBuilder, new()
        {
            MethodBuilder = new T();
            return this;
        }

        public abstract SerializationBuilder Configure<T>(DeserializerWithPosDlg<T> deserializeMethod);
        public abstract SerializationBuilder Configure<T>(DeserializerDlg<T> deserializeMethod);
        public abstract SerializationBuilder Configure<T>(SerializerDlg<T> serializeMethod);

        /// <summary>
        /// Call this method before calling Build() if you want to setup a store of any
        /// specified type using the configured <see cref="SerializationMethodBuilder"/>.
        /// of this instance.
        /// </summary>
        /// <param name="types">The types to setup.</param>
        /// <returns>The self instance to allow call chaining.</returns>
        public abstract SerializationBuilder SetupStoreOf(params Type[] types);

        /// <summary>
        /// Call this method before calling Build() if you want to setup the store of the
        /// specified type using the configured <see cref="SerializationMethodBuilder"/>.
        /// of this instance.
        /// </summary>
        ///<typeparam name="T">The type to setup.</typeparam>
        /// <returns>The self instance to allow call chaining.</returns>
        public abstract SerializationBuilder SetupStoreOf<T>();

        public abstract ISerDes Build();
    }
}
