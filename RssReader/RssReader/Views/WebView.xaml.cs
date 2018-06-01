using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RssReader.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebView : ContentPage
	{
	    private string model
	    {
	        get => BindingContext as string;
	        set => BindingContext = value;
	    }
		public WebView (string title, string url)
		{
			InitializeComponent ();
		    model = url;
		    Title = title;
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        await ProgressBar.ProgressTo(0.9, 900, Easing.SpringIn);
        }

	    private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
	    {
	        ProgressBar.IsVisible = true;
        }

	    private void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
	    {
	        ProgressBar.IsVisible = false;
        }
	}
}