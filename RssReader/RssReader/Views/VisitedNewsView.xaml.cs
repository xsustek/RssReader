using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BL;
using DAL;
using Realms;
using RssReader.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RssReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitedNewsView : ContentPage
    {
        public NewsViewModel ViewModel
        {
            get => BindingContext as NewsViewModel;
            set => BindingContext = value;
        }

        public VisitedNewsView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MyListView.BeginRefresh();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            if (e.Item is NewsDTO news)
            {
                await Navigation.PushAsync(new WebView(news.Title, news.Url));
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void FilterMenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ResourceFilterView { ViewModel = new ResourceFilterViewModel() });
        }
    }
}