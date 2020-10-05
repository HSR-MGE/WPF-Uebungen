// ReSharper disable StringLiteralTypo

namespace AsciiArt.ViewModel
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Input;

    using AsciiArt.Infrastructure;
    using AsciiArt.Model;

    public sealed class AsciiArtViewModel : BindableBase, IAsciiArtViewModel
    {
        private readonly Generator _generator;
        private readonly RelayCommand _chooseImageCommand;
        private readonly RelayCommand _createAsciiCommand;

        private int _fontSize;
        private int _lineWidth;
        private bool _isCalculating;
        private string _imagePath;
        private string _result;

        public AsciiArtViewModel()
        {
            _generator= new Generator();
            _chooseImageCommand = new RelayCommand(OnChooseImageCommandExecute);
            _createAsciiCommand = new RelayCommand(OnCreateAsciiCommandExecute, OnCreateAsciiCommandCanExecute);

            FontSize = 12;
            LineWidth = 80;
            ImagePath = string.Empty;
            Result = string.Empty;
            IsCalculating = false;
        }

        #region IAsciiArtViewModel

        public int FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value, nameof(FontSize));
        }

        public int LineWidth
        {
            get => _lineWidth;
            set => SetProperty(ref _lineWidth, value, nameof(LineWidth));
        }

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value, nameof(ImagePath));
        }

        public string Result
        {
            get => _result;
            private set => SetProperty(ref _result, value, nameof(Result));
        }

        public bool IsCalculating
        {
            get => _isCalculating;
            private set
            {
                SetProperty(ref _isCalculating, value, nameof(IsCalculating));
                _createAsciiCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand ChooseImageCommand => _chooseImageCommand;

        public ICommand CreateAsciiCommand => _createAsciiCommand;

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
