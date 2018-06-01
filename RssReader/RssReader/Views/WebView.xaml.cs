using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	    private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
	    {
	        ActivityIndicator.IsVisible = true;
	    }

	    private void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
	    {
	        ActivityIndicator.IsVisible = false;
        }
	}
}