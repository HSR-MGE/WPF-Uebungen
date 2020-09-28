namespace Aufgabe_3
{
    using System.Windows;

    public partial class App : Application
    {
        public const string HighlightColor = "HighlightColor";
        public const string BlueColor = "BlueColor";
        public const string GreenColor = "GreenColor";
        public const string RedColor = "RedColor";

        private static ThemeColor _themeColor;

        public static ThemeColor ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;

                var colorKey = BlueColor;

                switch (value)
                {
                    case ThemeColor.Blue:
                        colorKey = BlueColor;
                        break;

                    case ThemeColor.Green:
                        colorKey = GreenColor;
                        break;

                    case ThemeColor.Red:
                        colorKey = RedColor;
                        break;
                }

                Current.Resources[HighlightColor] = Current.Resources[colorKey];
            }
        }
    }
}