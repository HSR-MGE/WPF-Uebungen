// ReSharper disable StringLiteralTypo

namespace AsciiArt.Core.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using AsciiArt.Core.Infrastructure;
    using AsciiArt.Core.Model;
    using AsciiArt.Core.Service;

    public sealed class AsciiArtViewModel : BindableBase, IAsciiArtViewModel
    {
        private readonly Generator _generator;
        private readonly IPlatformSupport _platformSupport;
        private readonly RelayCommand _createAsciiCommand;

        private int _fontSize;
        private bool _isCalculating;
        private string _input;
        private string _result;

        public AsciiArtViewModel(IPlatformSupport platformSupport)
        {
            _platformSupport = platformSupport;
            _generator = new Generator();
            _createAsciiCommand = new RelayCommand(OnCreateAsciiCommandExecute, OnCreateAsciiCommandCanExecute);

            MinimumFontSize = 6;
            MaximumFontSize = 48;
            FontSize = 12;
            Input = string.Empty;
            Result = string.Empty;
            IsCalculating = false;
        }

        #region IAsciiArtViewModel

        public int MinimumFontSize { get; }

        public int MaximumFontSize { get; }

        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (value < MinimumFontSize)
                    return;

                if (value > MaximumFontSize)
                    return;

                SetProperty(ref _fontSize, value, nameof(FontSize));
            }
        }

        public string Input
        {
            get => _input;
            set => SetProperty(ref _input, value, nameof(Input));
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

        public ICommand CreateAsciiCommand => _createAsciiCommand;

        #endregion

        #region Private Methods

        public void OnCreateAsciiCommandExecute()
        {
            if (string.IsNullOrEmpty(Input))
            {
                ShowError("Eingabe fehlt", "Kann leider nichts berechnen: Keine Eingabe vorhanden");
                return;
            }
            
            Task.Run(() =>
            {
                try
                {
                    IsCalculating = true;

                    var result = _generator.GenerateAscii(Input);

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
            });
        }

        private bool OnCreateAsciiCommandCanExecute()
        {
            return !IsCalculating;
        }

        private void ShowError(string title, string msg)
        {
            _platformSupport.ShowError(title, msg);
        }

        #endregion
    }
}
