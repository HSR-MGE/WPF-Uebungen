namespace Aufgabe_2
{
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> eventArgs)
        {
            var r = (byte)ColorR.Value;
            var g = (byte)ColorG.Value;
            var b = (byte)ColorB.Value;

            TextR.Text = r.ToString();
            TextG.Text = g.ToString();
            TextB.Text = b.ToString();

            var color = Color.FromRgb(r, g, b);
            ColorArea.Fill = new SolidColorBrush(color);
            ColorLabel.Content = color.ToHexColor();
        }

        private void TextBox_RChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(ColorR, TextR);
        }

        private void TextBox_GChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(ColorG, TextG);
        }

        private void TextBox_BChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSliderFromTextBox(ColorB, TextB);
        }

        private static void UpdateSliderFromTextBox(RangeBase slider, TextBox textBox)
        {
            // Can be null during startup
            if (slider == null)
                return;

            slider.Value = byte.TryParse(textBox.Text, out var b) ? b : 0;
        }
    }
}
