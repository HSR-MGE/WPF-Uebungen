namespace AsciiArt.Core.Model
{
    using System;
    using Figgle;

    public class Generator
    {
        public string GenerateAscii(string text)
        {
            var output = string.Empty;

            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                output += FiggleFonts.Standard.Render(line);
            }

            return output;
        }
    }
}
