namespace Me.Amon.FilExp.OS
{
    public class OsFactory
    {
        private static IOs _Instance;

        public static IOs GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Windows.Win32();
            }
            return _Instance;
        }
    }
}
