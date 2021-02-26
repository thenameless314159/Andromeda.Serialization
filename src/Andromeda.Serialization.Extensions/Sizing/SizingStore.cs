using System;
using Andromeda.Serialization;

namespace Andromeda.Sizing
{
    internal static class SizingStore<TEndianness> where TEndianness : SerializationType
    {
        public static SizingMethodBuilder? Builder { get; private set; }

        public static void Setup(SizingMethodBuilder? builder)
        {
            if (Builder is not null) return;
            Builder = builder;
        }

        public static class Store<T>
        {
            public static SizeOfDlg<T> SizeOf
            {
                get => _sizeOfDlg ??= CreateSizeOfMethod();
                set
                {
                    if (_isSizeOfDlgAlreadySet) throw new InvalidOperationException($"Another sizeOf method has already been registered for {typeof(T).Name} !");
                    _isSizeOfDlgAlreadySet = true;
                    _sizeOfDlg = value;
                }
            }
            
            public static int Size
            {
                get => _sizeOf;
                set
                {
                    if (_isSizeOfAlreadySet) throw new InvalidOperationException($"Size of {typeof(T).Name} has already been registered !");
                    _isSizeOfAlreadySet = true;
                    _sizeOf = value;
                }
            }
            private static bool _isSizeOfDlgAlreadySet;
            private static bool _isSizeOfAlreadySet;
            private static SizeOfDlg<T>? _sizeOfDlg;
            private static int _sizeOf;

            public static SizeOfDlg<T> CreateSizeOfMethod() { _isSizeOfDlgAlreadySet = true; return Builder?.BuildSizeOf<T>()
                ?? throw new InvalidOperationException("SizingMethodBuilder is missing from the current " + nameof(SizingStore<TEndianness>)); }

            // trigger get to build from method builder
            public static void Setup() => _ = SizeOf;
        }
    }
}
