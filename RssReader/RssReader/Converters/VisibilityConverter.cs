using System;
using System.Globalization;
using Xamarin.Forms;

namespace RssReader.Converters
{
    public class VisitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool visited)
            {
                return visited ? Color.Gray : Color.Black;
            }

            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}