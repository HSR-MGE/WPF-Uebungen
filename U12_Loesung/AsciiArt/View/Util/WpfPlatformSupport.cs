// ReSharper disable CommentTypo

using System.Windows;

namespace AsciiArt.View.Util
{
    using System;
    using Microsoft.Win32;

    /// <summary>
    /// Enthält WPF-spezifische Hilfsmethoden. Wird vom View Model indirect über Callbacks
    /// verwendet, um plattform-spezifische Aktionen auszuführen. So bleibt das View Model
    /// frei von konkreten Technologien.
    /// </summary>
    public sealed class WpfPlatformSupport
    {
        /// <summary>
        /// Zeigt ein Datei-Öffnen Dialogfenster, erlaubt die Auswahl einer
        /// Bilddatei und weist das ausgewählte Bild der Property ImagePath zu.
        /// </summary>
        public string ChooseFile()
        {
            const string imagesFilter = "Image Files | *.jpg; *.jpeg; *.png; *.gif;";
            var userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var dlg = new OpenFileDialog
            {
                Filter = imagesFilter,
                InitialDirectory = userDirectory
            };

            var dialogResult = dlg.ShowDialog();
            if (dialogResult != true)
                return null;

            var chosenFile = string.IsNullOrEmpty(dlg.FileName) ? null : dlg.FileName;

            return chosenFile;
        }

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
