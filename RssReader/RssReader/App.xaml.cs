using System.Linq;
using System.Threading.Tasks;
using BL;
using DAL;
using Realms;
using RssReader.ViewModels;
using RssReader.Views;
using Xamarin.Forms;

namespace RssReader
{
	public partial class App : Application
	{
		public App ()
		{
		    InitializeComponent();
            MainPage = new NavigationPage(new Tabs());
		}

        private static async Task Init()
        {
            var realm = new RealmFactory();
            if(realm.Realm.All<Resource>().Any()) return;
            foreach (var resource in new[]
            {
                new Resource
                {
                    Name = "iDnes",
                    Url = "http://servis.idnes.cz/rss.aspx",
                    Display = true
                },
                new Resource
                {
                    Name = "iHned",
                    Url = "https://ihned.cz/?m=rss",
                    Display = true
                },
                new Resource
                {
                    Name = "Novinky",
                    Url = "https://www.novinky.cz/rss2/vase-zpravy/",
                    Display = true
                },
                new Resource
                {
                    Name = "ČT 24",
                    Url = "http://www.ceskatelevize.cz/ct24/rss/hlavni-zpravy",
                    Display = true
                }
            })
            {
                await realm.Realm.WriteAsync(r => { r.Add(resource); });
            }
        }

		protected override async void OnStart ()
		{
            // Handle when your app starts    
            await Init();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
