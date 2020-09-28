namespace Aufgabe_3
{
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ThemesButton_OnClick(object sender, RoutedEventArgs e)
        {
            var themeWindow = new ThemeWindow();
            themeWindow.Show();
        }
    }
}
