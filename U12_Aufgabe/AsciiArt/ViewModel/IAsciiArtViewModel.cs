// ReSharper disable CommentTypo
namespace AsciiArt.ViewModel
{
    using System;
    using System.Windows.Input;

    public interface IAsciiArtViewModel
    {
        // Properties für Bindings

        public int FontSize { get; set; }

        public int LineWidth { get; set; }

        public string ImagePath { get; set; }

        public string Result { get; }

        public bool IsCalculating { get; }

        // Commands für Bindings

        public ICommand ChooseImageCommand { get; }

        public ICommand CreateAsciiCommand { get; }

        // Callbacks für WPF-spezifische Logik

        public Func<string> OnChooseFile { get; set; }

        public Action<string, string> OnShowError { get; set; }
    }
}
