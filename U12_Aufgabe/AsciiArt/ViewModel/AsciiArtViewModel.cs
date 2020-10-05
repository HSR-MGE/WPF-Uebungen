// ReSharper disable StringLiteralTypo

namespace AsciiArt.ViewModel
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Input;

    using AsciiArt.Infrastructure;
    using AsciiArt.Model;

    public sealed class AsciiArtViewModel : IAsciiArtViewModel
    {
        private readonly Generator _generator;

        public AsciiArtViewModel()
        {
            _generator= new Generator();

            ChooseImageCommand = new RelayCommand(() => {}, () => false);
            CreateAsciiCommand = new RelayCommand(() => {}, () => false);

            FontSize = 12;
            LineWidth = 80;
            ImagePath = string.Empty;
            Result = string.Empty;
            IsCalculating = false;
        }

        #region IAsciiArtViewModel

        public int FontSize { get; set; }

        public int LineWidth { get; set; }

        public string ImagePath { get; set; }

        public string Result { get; private set; }

        public bool IsCalculating { get; private set; }

        public ICommand ChooseImageCommand { get; }

        public ICommand CreateAsciiCommand { get; }

        public Func<string> OnChooseFile { get; set; } = () => throw new InvalidOperationException($"Zuweisung fehlt: {nameof(OnChooseFile)}");

        public Action<string, string> OnShowError { get; set; } = (t, m) => throw new InvalidOperationException($"Zuweisung fehlt: {nameof(OnShowError)}");

        #endregion

        #region Private Methods

        private void OnChooseImageCommandExecute()
        {
            var filename = OnChooseFile?.Invoke();

            if (!string.IsNullOrEmpty(filename))
            {
                ImagePath = filename;
            }
        }

        public void OnCreateAsciiCommandExecute()
        {
            if (string.IsNullOrEmpty(ImagePath))
            {
                ShowError("Quelldatei fehlt", "Kann leider nichts berechnen: Keine Quelldatei angegeben");
                return;
            }

            if (!File.Exists(ImagePath))
            {
                ShowError("Quelldatei nicht verfügbar", "Kann leider nichts berechnen: Quelldatei nicht gefunden");
                return;
            }

            try
            {
                IsCalculating = true;

                var imageAsBitmap = (Bitmap) Image.FromFile(ImagePath);
                var result = _generator.GenerateFrom(imageAsBitmap, LineWidth);

                Result = result;
            }
            catch (Exception e)
            {
                ShowError("Fehler aufgetreten", $"Berechnung fehlgeschlagen. Ursache: {e.Message}");
            }
            finally
            {
                IsCalculating = false;
            }
        }

        private bool OnCreateAsciiCommandCanExecute()
        {
            return !IsCalculating;
        }

        private void ShowError(string title, string msg)
        {
            OnShowError?.Invoke(title, msg);
        }

        #endregion
    }
}
