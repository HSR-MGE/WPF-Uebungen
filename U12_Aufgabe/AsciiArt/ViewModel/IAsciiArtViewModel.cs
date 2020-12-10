// ReSharper disable CommentTypo
namespace AsciiArt.ViewModel
{
    using System;
    using System.Windows.Input;

    public interface IAsciiArtViewModel
    {
        // Properties für Bindings

        int FontSize { get; set; }

        int LineWidth { get; set; }

        string ImagePath { get; set; }

        string Result { get; }

        bool IsCalculating { get; }

        // Commands für Bindings

        ICommand ChooseImageCommand { get; }

        ICommand CreateAsciiCommand { get; }

        // Callbacks für WPF-spezifische Logik

        Func<string> OnChooseFile { get; set; }

        Action<string, string> OnShowError { get; set; }
    }
}
