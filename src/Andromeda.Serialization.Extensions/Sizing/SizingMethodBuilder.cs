namespace Andromeda.Sizing
{
    public delegate int SizeOfDlg<in T>(ISizing sizing, T value);

    public abstract class SizingMethodBuilder
    {
        public abstract SizeOfDlg<T> BuildSizeOf<T>();
    }
}
