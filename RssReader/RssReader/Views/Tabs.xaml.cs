using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
                if (!Children.Any())
                {
                    Children.Add(new NewsList
                    {
                        ViewModel = new NewsViewModel(false)
                    });
                    Children.Add(new VisitedNewsView
                    {
                        ViewModel = new NewsViewModel(true)
                    });
                }
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
                if (!Children.Any())
                {
                    Children.Add(new NewsList
                    {
                        ViewModel = new NewsViewModel(false)
                    });
                    Children.Add(new VisitedNewsView
                    {
                        ViewModel = new NewsViewModel(true)
                    });
                }
            }
        }
    }
}