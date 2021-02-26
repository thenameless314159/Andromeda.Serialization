using System;
using System.Reflection;
using Andromeda.Serialization;

namespace Andromeda.Sizing
{
    internal sealed class SizingBuilder<TEndianness> : SizingBuilder where TEndianness : SerializationType
    {
        public override SizingBuilder Configure<T>(SizeOfDlg<T> sizeOfMethod)
        {
            SizingStore<TEndianness>.Store<T>.SizeOf = sizeOfMethod;
            return this;
        }

        public override SizingBuilder Configure<T>(int sizeOfT)
        {
            SizingStore<TEndianness>.Store<T>.Size = sizeOfT;
            return this;
        }

        private static void SetupStore<T>() => SizingStore<TEndianness>.Store<T>.Setup();
        public override SizingBuilder SetupStoreOf(params Type[] types)
        {
            SizingStore<TEndianness>.Setup(MethodBuilder);
            var setupMi = typeof(SizingBuilder<TEndianness>).GetMethod(
                nameof(SetupStore), BindingFlags.NonPublic)!;

            foreach (var type in types) setupMi
                .MakeGenericMethod(type)
                .Invoke(this, Array.Empty<object>());

            return this;
        }

        public override SizingBuilder SetupStoreOf<T>() {
            SizingStore<TEndianness>.Setup(MethodBuilder);
            SizingStore<TEndianness>.Store<T>.Setup();
            return this;
        }

        public override ISizing Build() { SizingStore<TEndianness>.Setup(MethodBuilder); return new DefaultSizing<TEndianness>(); }
    }
}
