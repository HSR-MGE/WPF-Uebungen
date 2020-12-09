namespace AsciiArt.WPF.Designer
{
    using AsciiArt.Core.Service;
    using AsciiArt.Core.ViewModel;
    using AsciiArt.WPF.Service;

    public static class DesignerData
    {
        private static IPlatformSupport PlatformSupport { get; } = new WpfPlatformSupport();

        public static AsciiArtViewModel ViewModel { get; } = new AsciiArtViewModel(PlatformSupport);
    }
}
