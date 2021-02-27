namespace Andromeda.Sizing
{
    public static class SizingBuilderExtensions
    {
        public static SizingBuilder ConfigureDefault(this SizingBuilder builder) => builder
            .Configure<bool>(sizeof(bool))
            .Configure<char>(sizeof(char))
            .Configure<byte>(sizeof(byte))
            .Configure<sbyte>(sizeof(sbyte))
            .Configure<short>(sizeof(short))
            .Configure<ushort>(sizeof(ushort))
            .Configure<int>(sizeof(int))
            .Configure<uint>(sizeof(uint))
            .Configure<long>(sizeof(long))
            .Configure<ulong>(sizeof(ulong))
            .Configure<float>(sizeof(float))
            .Configure<double>(sizeof(double))
            .Configure<decimal>(sizeof(decimal));
    }
}
