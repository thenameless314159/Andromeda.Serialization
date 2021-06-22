using System;
using Andromeda.Sizing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Andromeda.Serialization
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultSerialization(this IServiceCollection services, Action<SerializationBuilder> configure,
            bool registerSerDesOnly = false) 
        {
            var builder = SerializationBuilder.CreateDefault();
            configure(builder);

            var serDes = builder.Build();
            services.TryAddSingleton(serDes);
            if (registerSerDesOnly) return services;

            services.TryAddSingleton<IDeserializer>(serDes);
            services.TryAddSingleton<ISerializer>(serDes);
            return services;
        }

        public static IServiceCollection AddDefaultSizing(this IServiceCollection services, Action<SizingBuilder> configure)
        {
            var builder = SizingBuilder.CreateDefault();
            configure(builder);

            services.TryAddSingleton(builder.Build());
            return services;
        }

        public static IServiceCollection AddSerialization(this IServiceCollection services, SerializationType endianness, Action<SerializationBuilder> configure,
            bool registerSerDesOnly = false)
        {
            var builder = SerializationBuilder.Create(endianness);
            configure(builder);

            var serDes = builder.Build();
            services.TryAddSingleton(serDes);
            if (registerSerDesOnly) return services;

            services.TryAddSingleton<IDeserializer>(serDes);
            services.TryAddSingleton<ISerializer>(serDes);
            return services;
        }

        public static IServiceCollection AddSizing(this IServiceCollection services, SerializationType endianness, Action<SizingBuilder> configure)
        {
            var builder = SizingBuilder.Create(endianness);
            configure(builder);

            services.TryAddSingleton(builder.Build());
            return services;
        }

        public static IServiceCollection AddSerializationFor<TEndianness>(this IServiceCollection services, Action<SerializationBuilder> configure, 
            bool registerSerDesOnly = false) where TEndianness : SerializationType, new()
        {
            var builder = SerializationBuilder.CreateFor<TEndianness>();
            configure(builder);

            var serDes = builder.Build();
            services.TryAddSingleton(serDes);
            if (registerSerDesOnly) return services;

            services.TryAddSingleton<IDeserializer>(serDes);
            services.TryAddSingleton<ISerializer>(serDes);
            return services;
        }

        public static IServiceCollection AddSizingFor<TEndianness>(this IServiceCollection services, Action<SizingBuilder> configure)
            where TEndianness : SerializationType
        {
            var builder = SizingBuilder.CreateFor<TEndianness>();
            configure(builder);

            services.TryAddSingleton(builder.Build());
            return services;
        }
    }
}
