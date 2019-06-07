using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Me.Amon.Uc
{
    public class StringToImageSourceConverter : IValueConverter
    {
        private static BitmapImage _DefImage;

        static StringToImageSourceConverter()
        {
            _DefImage = new BitmapImage();
        }

        #region Converter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = (string)value;
            if (!string.IsNullOrEmpty(path))
            {
                return new BitmapImage(new Uri(path, UriKind.Absolute));
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        #endregion
    }
}
