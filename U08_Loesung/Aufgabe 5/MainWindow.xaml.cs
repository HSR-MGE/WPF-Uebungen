using System.Windows;

namespace Aufgabe_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SayHelloButton_OnClick(object sender, RoutedEventArgs e)
        {
            Greeting.Text = $"Hello, {NameInput.Text}!";
            NameInput.Text = string.Empty;
        }
    }
}
