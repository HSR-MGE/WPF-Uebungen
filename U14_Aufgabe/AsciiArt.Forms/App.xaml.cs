namespace AsciiArt.Forms
{
    using AsciiArt.Core.ViewModel;
    using AsciiArt.Core.Infrastructure;
    using AsciiArt.Forms.Pages;
    using AsciiArt.Forms.Service;

    using Xamarin.Forms;

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            RelayCommand.Dispatch = Device.BeginInvokeOnMainThread;

            var platformSupport = new XamarinFormsPlatformSupport();
            var viewModel = new AsciiArtViewModel(platformSupport);

            MainPage = new AsciiArtPage(viewModel);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
