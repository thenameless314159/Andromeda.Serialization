﻿using System;
using System.Reflection;
using Andromeda.Serialization;

namespace Andromeda.Sizing
{
    public abstract class SizingBuilder
    {
        private static readonly MethodInfo _createForMi = typeof(SizingBuilder).GetMethod(nameof(Create), Type.EmptyTypes)!;
        private static SizingBuilder CreateFrom(SerializationType type) => (SizingBuilder)
            _createForMi.MakeGenericMethod(type.GetType()).Invoke(null, Array.Empty<object>())!;

        public static SizingBuilder CreateDefault() => BitConverter.IsLittleEndian
            ? new SizingBuilder<LittleEndian>().ConfigureDefault()
            : new SizingBuilder<BigEndian>().ConfigureDefault();

        public static SizingBuilder Create(SerializationType type) => type switch {
            LittleEndian => CreateFor<LittleEndian>().ConfigureDefault(),
            BigEndian => CreateFor<BigEndian>().ConfigureDefault(),
            Other => CreateFor<Other>(),
            _ => CreateFrom(type)
        };

        public static SizingBuilder CreateFor<T>() where T : SerializationType =>
            new SizingBuilder<T>();

        protected SizingBuilder(SizingMethodBuilder? methodBuilder) =>
            MethodBuilder = methodBuilder;

        public SizingMethodBuilder? MethodBuilder { get; set; }

        public SizingBuilder Use(SizingMethodBuilder builder)
        {
            MethodBuilder = builder;
            return this;
        }
        
        public SizingBuilder Use<T>(T builder) where T : SizingMethodBuilder
        {
            MethodBuilder = builder;
            return this;
        }
        
        public SizingBuilder Use<T>() where T : SizingMethodBuilder, new()
        {
            MethodBuilder = new T();
            return this;
        }

        public abstract SizingBuilder Configure<T>(SizeOfDlg<T> sizeOfMethod);
        public abstract SizingBuilder Configure<T>(int sizeOfT);

        /// <summary>
        /// Call this method before calling Build() if you want to setup a store of any
        /// specified type using the configured <see cref="SizingMethodBuilder"/>.
        /// of this instance.
        /// </summary>
        /// <param name="types">The types to setup.</param>
        /// <returns>The self instance to allow call chaining.</returns>
        public abstract SizingBuilder SetupStoreOf(params Type[] types);

        /// <summary>
        /// Call this method before calling Build() if you want to setup the store of the
        /// specified type using the configured <see cref="SizingMethodBuilder"/>.
        /// of this instance.
        /// </summary>
        ///<typeparam name="T">The type to setup.</typeparam>
        /// <returns>The self instance to allow call chaining.</returns>
        public abstract SizingBuilder SetupStoreOf<T>();

        public abstract ISizing Build();
    }
}
