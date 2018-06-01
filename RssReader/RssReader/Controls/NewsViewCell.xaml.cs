using System;
using System.Net;
using BL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WebView = RssReader.Views.WebView;

namespace RssReader.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsViewCell : ViewCell
    {
        public static readonly BindableProperty NewsProperty =
            BindableProperty.Create(nameof(News), typeof(NewsDTO), typeof(NewsViewCell), null);

        public NewsDTO News
        {
            get => (NewsDTO) GetValue(NewsProperty);
            set => SetValue(NewsProperty, value);
        }

        public NewsViewCell()
        {
            InitializeComponent();
        }
    }
}