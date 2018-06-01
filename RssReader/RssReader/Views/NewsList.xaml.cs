using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
    public partial class NewsList : ContentPage
    {
        public NewsViewModel ViewModel
        {
            get => BindingContext as NewsViewModel;
            set => BindingContext = value;
        }

        public NewsList()
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
                var indexOf = news.Description.IndexOf("<", StringComparison.Ordinal);
                var description = indexOf > 0
                                      ? news.Description.Substring(0, indexOf)
                                      : news.Description;

                if (await DisplayAlert(news.Title,
                                       WebUtility.HtmlDecode(description),
                                       "Visit", "Cancel"))
                {
                    news.Visited = true;
                    ViewModel.Save(news);
                    await Navigation.PushAsync(new WebView(news.Title, news.Url));
                }
            }

            //Deselect Item
            ((ListView) sender).SelectedItem = null;
        }

        private async void FilterMenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ResourceFilterView
            {
                ViewModel = new ResourceFilterViewModel()
            });
        }

        private async void AddMenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddResourcePage
            {
                ViewModel = new AddResourceViewModel()
            });
        }
    }
}