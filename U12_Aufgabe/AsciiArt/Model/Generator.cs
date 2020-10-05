// ReSharper disable UnusedMember.Local
namespace AsciiArt.Model
{
    using System;
    using System.Drawing;
    using System.Text;

    /// <summary>
    /// Produces nice ASCII Art pictures from a standard .NET bitmap
    /// </summary>
    /// <remarks>
    /// Thanks to Sean Sexton, from which this code was shamelessly taken
    /// and adapted:
    /// http://csharp.2000things.com/2012/12/25/743-ascii-art-generator/
    /// </remarks>
    public class Generator
    {
        // Typical width/height for ASCII characters
        private const double FontAspectRatio = 0.6;

        // Available character set, ordered by decreasing intensity (brightness)
        private static string OutputCharSet => OutputCharSet2;

        private const string OutputCharSet1 = "@%#*+=-:. ";
        private const string OutputCharSet2 = "MNFVI*:.";
        private const string OutputCharSet3 = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";

        /// <summary>
        /// creates an ascii art image [outputWidth] characters wide
        /// from the given input image 
        /// </summary>
        /// <param name="bmInput"></param>
        /// <param name="outputWidth"></param>
        /// <returns></returns>
        public string GenerateFrom(Bitmap bmInput, int outputWidth)
        {
            // we cannot produce more characters per line than the actual number of pixels
            outputWidth = Math.Min(outputWidth, bmInput.Width);

            // pixelChunkWidth/pixelChunkHeight - size of a chunk of pixels that will
            // map to 1 character.  These are doubles to avoid progressive rounding
            // error.
            var pixelChunkWidth = (double)bmInput.Width / (double)outputWidth;
            var pixelChunkHeight = pixelChunkWidth / FontAspectRatio;

            // Calculate output height to capture entire image
            var outputHeight = (int)Math.Round(bmInput.Height / pixelChunkHeight);

            // Generate output image, row by row
            var pixelOffSetTop = 0.0;
            var sbOutput = new StringBuilder();

            for (int row = 1; row <= outputHeight; row++)
            {
                var pixelOffSetLeft = 0.0;

                for (int col = 1; col <= outputWidth; col++)
                {
                    // Calculate brightness for this set of pixels by averaging
                    // brightness across all pixels in this pixel chunk
                    var brightSum = 0.0;
                    var pixelCount = 0;
                    for (int pixelLeftInd = 0; pixelLeftInd < (int)pixelChunkWidth; pixelLeftInd++)
                        for (int pixelTopInd = 0; pixelTopInd < (int)pixelChunkHeight; pixelTopInd++)
                        {
                            // Each call to GetBrightness returns value between 0.0 and 1.0
                            var x = (int)pixelOffSetLeft + pixelLeftInd;
                            var y = (int)pixelOffSetTop + pixelTopInd;
                            if ((x < bmInput.Width) && (y < bmInput.Height))
                            {
                                brightSum += bmInput.GetPixel(x, y).GetBrightness();
                                pixelCount++;
                            }
                        }

                    // Average brightness for this entire pixel chunk, between 0.0 and 1.0
                    var pixelChunkBrightness = brightSum / pixelCount;

                    // Target character is just relative position in ordered set of output characters
                    var outputIndex = (int)Math.Floor(pixelChunkBrightness * OutputCharSet.Length);
                    if (outputIndex == OutputCharSet.Length)
                        outputIndex--;

                    var targetChar = OutputCharSet[outputIndex];

                    sbOutput.Append(targetChar);

                    pixelOffSetLeft += pixelChunkWidth;
                }
                sbOutput.AppendLine();
                pixelOffSetTop += pixelChunkHeight;
            }

            return sbOutput.ToString();
        }
    }
}