using RssReader.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RssReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourceFilterView : ContentPage
    {
        public ResourceFilterViewModel ViewModel
        {
            get => BindingContext as ResourceFilterViewModel;
            set => BindingContext = value;
        }
        

        public ResourceFilterView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Load();
        }
    }
}
