namespace AsciiArt
{
    using System.Windows;

    using AsciiArt.View.Util;
    using AsciiArt.View.Windows;
    using AsciiArt.ViewModel;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var platformSupport = new WpfPlatformSupport();

            IAsciiArtViewModel viewModel = new AsciiArtViewModel
            {
                OnChooseFile = platformSupport.ChooseFile,
                OnShowError = platformSupport.ShowError
            };

            MainWindow = new AsciiArtWindow();

            MainWindow.Show();
        }
    }
}
