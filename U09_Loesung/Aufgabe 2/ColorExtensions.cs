namespace Aufgabe_2
{
    using System.Windows.Media;

    /// <summary>
    /// Class that attaches methods (currently only the method <see cref="ToHexColor"/>) to
    /// objects of type Color (WPF class). Extension methods are a C# concept, see:
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
    /// <br /><br />
    /// Using method syntax, you can use the method as follows:
    ///
    /// <code>
    /// var rgbString = ColorExtensions.ToHexColor(Colors.Blue);
    /// </code>
    ///
    /// Using the more compact extension method syntax, this transforms to just
    /// calling the <see cref="ToHexColor"/>-method on the Color object directly:
    ///
    /// <code>
    /// var rgbString = Colors.Blue.ToHexColor();
    /// </code>
    /// </summary>
    public static class ColorExtensions
    {
        public static string ToHexColor(this Color color, bool alpha = false)
        {
            var rgbString = $"{color.R:X2}{color.G:X2}{color.B:X2}";
            if (alpha) rgbString = $"{color.A:X2}{rgbString}";
            return $"#{rgbString}";
        }
    }
}
