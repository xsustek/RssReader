using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Realms;
using Xamarin.Forms;

namespace RssReader
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

		    RssResource.Create();
		    lbl.Text = RssResource.Count().ToString();
		}
	}
}
