// ReSharper disable CommentTypo
namespace AsciiArt.Core.ViewModel
{
    using System.Windows.Input;

    public interface IAsciiArtViewModel
    {
        // Properties für Bindings

        int FontSize { get; set; }

        string Input { get; set; }

        string Result { get; }

        bool IsCalculating { get; }

        // Commands für Bindings

        ICommand CreateAsciiCommand { get; }
    }
}
