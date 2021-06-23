using System.Collections.Generic;

namespace Andromeda.Sizing
{
    public interface ISizing
    {
        int SizeOf<T>();
        int SizeOf<T>(T value);
        int SizeOfValues<T>(IEnumerable<T> values);
    }
}
