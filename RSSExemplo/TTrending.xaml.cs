using MarcTron.Plugin;
using RSSExemplo.Models;
using RSSExemplo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("Montserrat-Regular.otf", Alias = "ms1")]
[assembly: ExportFont("Montserrat-Bold.otf", Alias = "ms2")]
namespace RSSExemplo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TTrending : ContentPage
    {
        public TTrending()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new TwitterViewModel();
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

        private void news_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RSSWebViewPage("https://twitter.com/search?q=%23Bitcoin"));
        }

        private void crypto_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RSSWebViewPage("https://twitter.com/search?q=%23Cryptocurrency"));
        }

        private void eth_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RSSWebViewPage("https://twitter.com/search?q=%23Ethereum"));
        }
    }
}