namespace AsciiArt.WPF
{
    using System.Windows;

    using AsciiArt.Core.Infrastructure;
    using AsciiArt.Core.ViewModel;
    using AsciiArt.WPF.Service;
    using AsciiArt.WPF.Windows;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RelayCommand.Dispatch = Dispatcher.Invoke;

            var platformSupport = new WpfPlatformSupport();
            var viewModel = new AsciiArtViewModel(platformSupport);

            MainWindow = new AsciiArtWindow(viewModel);
            MainWindow.Show();
        }
    }
}
