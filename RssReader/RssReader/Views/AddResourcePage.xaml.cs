using System;
using System.Net.Http;
using System.Threading.Tasks;
using BL.Network;
using RssReader.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RssReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddResourcePage : ContentPage
    {
        public AddResourceViewModel ViewModel
        {
            get => BindingContext as AddResourceViewModel;
            set => BindingContext = value;
        }

        public AddResourcePage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (!await Validate()) return;

            await ViewModel.Add();

            await Navigation.PopModalAsync();
        }

        private async Task<bool> Validate()
        {
            if (string.IsNullOrEmpty(ViewModel.Resource.Name))
            {
                await DisplayAlert("Name is empty", "Name of the feed cannot be empty", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(ViewModel.Resource.Url))
            {
                await DisplayAlert("Url is empty", "Url of the feed cannot be empty", "OK");
                return false;
            }

            try
            {
                var saveNetwork = new AppHttpClient();
                var urlResponse = await saveNetwork.Send(async c
                    => await c.SendAsync(
                        new HttpRequestMessage(HttpMethod.Head, ViewModel.Resource.Url)));

                if (!urlResponse.IsSuccessStatusCode)
                {
                    await DisplayAlert("Url is not valid",
                        "Url of the feed must be existing web address", "OK");
                    return false;
                }
            }
            catch
            {
                await DisplayAlert("Url is not valid",
                    "Url of the feed must be existing web address",
                    "OK");
                return false;
            }


            return true;
        }

        private async void CalcelButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}