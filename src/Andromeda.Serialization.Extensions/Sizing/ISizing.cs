using System.Collections.Generic;

namespace Andromeda.Sizing
{
    public interface ISizing
    {
        int SizeOf<T>();
        int SizeOf<T>(in T value);
        int SizeOfValues<T>(in IEnumerable<T> values);
    }
}
