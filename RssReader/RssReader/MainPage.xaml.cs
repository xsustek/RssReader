using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BL;
using DAL;
using Realms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RssReader
{
    public partial class MainPage : ContentPage
    {
        public bool IsLoading;
        public MainPage()
        {
            InitializeComponent();
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            lbl.Text = (await Rss()).ToString();
        }

        private async Task<int> Rss()
        {
            ActivityIndicator.IsRunning = true;
            var respo = new RssResourceRepository<RssItem>();
            var clien = new HttpClient();
            using (var res = await clien.GetStreamAsync("https://ihned.cz/?m=rss"))
            {
                var rss = XDocument.Load(res);
                var tasks = rss.Descendants("item").Select(i => new RssItem
                {
                    Title = i.Element("title").Value,
                    Link = i.Element("link").Value,
                    Description = i.Element("description").Value
                }).Select(q => respo.Create(q));
                await Task.WhenAll(tasks.ToArray());
                var result = respo.All().Count;
                ActivityIndicator.IsRunning = false;
                return result;
            }
        }
    }
}