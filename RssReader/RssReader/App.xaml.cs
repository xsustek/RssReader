using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL;
using Xamarin.Forms;

namespace RssReader
{
	public partial class App : Application
	{

		public App ()
		{
		    InitializeComponent();
			MainPage = new RssReader.MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
            RealmLificycle.Create();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
            RealmLificycle.Close();
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
            RealmLificycle.Create();
		}
	}
}
