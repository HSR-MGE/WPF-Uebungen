namespace AsciiArt.Core.Service
{
    public interface IPlatformSupport
    {
        string ChooseFile();

        void ShowError(string title, string msg);
    }
}
