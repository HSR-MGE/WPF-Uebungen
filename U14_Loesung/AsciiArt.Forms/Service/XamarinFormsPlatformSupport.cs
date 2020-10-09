namespace AsciiArt.Forms.Service
{
    using AsciiArt.Core.Service;

    using Xamarin.Forms;

    public sealed class XamarinFormsPlatformSupport : IPlatformSupport
    {
        public void ShowError(string title, string msg)
        {
            Application.Current.MainPage.DisplayAlert(title, msg, "OK");
        }
    }
}
