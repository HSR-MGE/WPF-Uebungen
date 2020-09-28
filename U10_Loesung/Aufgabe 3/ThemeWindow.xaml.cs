
namespace Aufgabe_3
{
    using System.Windows;

    public partial class ThemeWindow : Window
    {
        public ThemeWindow()
        {
            InitializeComponent();

            switch (App.ThemeColor)
            {
                case ThemeColor.Blue:
                    BlueOption.IsChecked = true;
                    break;

                case ThemeColor.Green:
                    GreenOption.IsChecked = true;
                    break;
                case ThemeColor.Red:
                    RedOption.IsChecked = true;
                    break;
            }
        }

        private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.BlueOption.IsChecked.Value)
            {
                App.ThemeColor = ThemeColor.Blue;
            } else if (this.GreenOption.IsChecked.Value)
            {
                App.ThemeColor = ThemeColor.Green;
            }
            else
            {
                App.ThemeColor = ThemeColor.Red;
            }

            this.Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
