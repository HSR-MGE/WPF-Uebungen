namespace Aufgabe_2
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public sealed class RGBToStringConverter : IMultiValueConverter
    {
        private readonly RGBToColorConverter toColor = new RGBToColorConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)toColor.Convert(values, targetType, parameter, culture);
            var colorString = color.ToHexColor();

            return colorString;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}