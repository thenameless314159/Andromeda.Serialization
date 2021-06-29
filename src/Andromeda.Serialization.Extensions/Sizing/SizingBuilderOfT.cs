using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public override SizingBuilder SetupStoreOf(IEnumerable<Type> types, bool parallelSetup = false)
        {
            SizingStore<TEndianness>.Setup(MethodBuilder);
            if (!parallelSetup) {
                foreach (var type in types) SizingStore<TEndianness>.SetupStoreOf(type);
                return this;
            }

            Parallel.ForEach(types, SizingStore<TEndianness>.SetupStoreOf);
            return this;
        }

        public override SizingBuilder SetupStoreOf<T>() {
            SizingStore<TEndianness>.Setup(MethodBuilder);
            SizingStore<TEndianness>.SetupStore<T>();
            return this;
        }

        public override ISizing Build() { SizingStore<TEndianness>.Setup(MethodBuilder); return new DefaultSizing<TEndianness>(); }
    }
}
