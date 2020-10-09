// ReSharper disable CommentTypo

namespace AsciiArt.WPF.Service
{
    using System.Windows;

    using AsciiArt.Core.Service;
    
    public sealed class WpfPlatformSupport : IPlatformSupport
    {
        /// <summary>
        /// Zeigt eine Fehlermeldung in einer Message Box an
        /// </summary>
        /// <param name="title">Der Titel für die Fehlermeldung</param>
        /// <param name="msg">Die Nachricht für die Fehlermeldung</param>
        public void ShowError(string title, string msg)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
