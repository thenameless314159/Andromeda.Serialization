using System.Collections.Generic;
using Andromeda.Serialization;

namespace Andromeda.Sizing
{
    internal sealed class DefaultSizing<TEndianness> : ISizing where TEndianness : SerializationType
    {
        public int SizeOf<T>(in T value) => SizingStore<TEndianness>.Store<T>.SizeOf(this, in value);

        public int SizeOfArray<T>(T[] values)
        {
            var result = 0;
            for (var i = 0; i < values.Length; i++) { var value = values[i];
                result += SizingStore<TEndianness>.Store<T>.SizeOf(this, in value);
            }

            return result;
        }

        public int SizeOf<T>() => SizingStore<TEndianness>.Store<T>.Size;

        public int SizeOfValues<T>(IEnumerable<T> values)
        {
            var result = 0;
            foreach (var value in values) {
                result += SizingStore<TEndianness>.Store<T>.SizeOf(this, in value);
            }
            return result;
        }
    }
}
