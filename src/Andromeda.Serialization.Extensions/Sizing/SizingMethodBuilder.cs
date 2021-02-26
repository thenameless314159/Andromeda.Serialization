namespace Andromeda.Sizing
{
    public delegate int SizeOfDlg<T>(ISizing sizing, in T value);

    public abstract class SizingMethodBuilder
    {
        public abstract SizeOfDlg<T> BuildSizeOf<T>();
    }
}
