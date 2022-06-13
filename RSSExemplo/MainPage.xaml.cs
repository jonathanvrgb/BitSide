using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcTron.Plugin;
using RSSExemplo.Models;
using RSSExemplo.ViewModels;
using Xamarin.Forms;
[assembly: ExportFont("Montserrat-Regular.otf", Alias = "ms1")]
[assembly: ExportFont("Montserrat-Bold.otf", Alias = "ms2")]
namespace RSSExemplo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new MainViewModel();
        }

        async override protected void OnAppearing()
        {
            base.OnAppearing();
            CrossMTAdmob.Current.ShowInterstitial();
            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-5325371552579828/2601532161");
            base.OnAppearing();
        }

        void ListView_OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var selectedItem = (RssData)e.Item;
            Navigation.PushAsync(new RSSWebViewPage(selectedItem.Link));
        }

        private async void news_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void trending_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TTrending());
        }

        private async void market_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MarketPrices());
        }
    }
}
