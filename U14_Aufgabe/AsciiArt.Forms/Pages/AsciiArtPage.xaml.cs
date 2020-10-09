using System.ComponentModel;

namespace AsciiArt.Forms.Pages
{
    using AsciiArt.Core.ViewModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsciiArtPage : TabbedPage
    {
        public AsciiArtPage(AsciiArtViewModel viewModel)
        {
            InitializeComponent();

            this.BindingContext = viewModel;

            viewModel.PropertyChanged += DoOnViewModelPropertyChanged;
        }

        private void DoOnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAsciiArtViewModel.Result))
            {
                Device.BeginInvokeOnMainThread(() => this.CurrentPage = Children[1]);
            }
        }
    }
}