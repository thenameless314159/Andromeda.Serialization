using System.Collections.Generic;

namespace Andromeda.Sizing
{
    public interface ISizing
    {
        int SizeOf<T>();
        int SizeOf<T>(in T value);
        int SizeOfArray<T>(T[] values);
        int SizeOfValues<T>(IEnumerable<T> values);
    }
}
