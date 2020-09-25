using System.Windows.Media;

namespace Aufgabe_4
{
    using System;
    using System.Windows.Markup;

    public sealed class RGBString : MarkupExtension
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public override object ProvideValue(IServiceProvider s)
        {
            return $"#{R:X2}{G:X2}{B:X2}";
        }
    }

    public sealed class RGBColor : MarkupExtension
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public override object ProvideValue(IServiceProvider s)
        {
            return Color.FromRgb(R, G, B);
        }
    }
}
