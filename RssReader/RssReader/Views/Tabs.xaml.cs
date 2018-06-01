using System.Linq;
using Plugin.Connectivity;
using RssReader.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RssReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tabs : TabbedPage
    {
        public Tabs()
        {
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        private async void Current_ConnectivityChanged(object sender,
            Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (!e.IsConnected)
            {
                await DisplayAlert("Error", "No internet connection", "OK");
                Current_ConnectivityChanged(sender, e);
            }
            else
            {
                InitTabs();
            }
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Error", "No internet connection", "OK");
                OnAppearing();
            }
            else
            {
                InitTabs();
            }
        }

        private void InitTabs()
        {
            if (!Children.Any())
            {
                Children.Add(new NewsList
                {
                    Title = "Feed",
                    Icon = "news.png",
                    ViewModel = new NewsViewModel(false)
                });
                Children.Add(new NewsList
                {
                    Title = "Visited",
                    Icon = "eye.png",
                    ViewModel = new NewsViewModel(true)
                });
            }
        }
    }
}