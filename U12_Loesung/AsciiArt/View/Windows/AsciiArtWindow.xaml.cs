using AsciiArt.ViewModel;

namespace AsciiArt.View.Windows
{
    using System.Windows;

    public partial class AsciiArtWindow : Window
    {
        public AsciiArtWindow(IAsciiArtViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
