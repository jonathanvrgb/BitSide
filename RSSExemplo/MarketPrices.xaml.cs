using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("Montserrat-Regular.otf", Alias = "ms1")]
[assembly: ExportFont("Montserrat-Bold.otf", Alias = "ms2")]
namespace RSSExemplo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarketPrices : ContentPage
    {
        public MarketPrices()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            string lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            bool hasKey = Preferences.ContainsKey("cur");
            if (hasKey == false)
            {
            if (lang == "pt")
            {
                picker.Title = "SELECIONE A SUA MOEDA";
                picker.Items.Add("Dólar Americano (U$)");
                picker.Items.Add("Real Brasileiro (BRL)");
                picker.Items.Add("Euro (€)");
            }
            else
            {
                picker.Title = "SELECT YOUR CURRENCY";
                picker.Items.Add("US Dollar (U$)");
                picker.Items.Add("Brazilian Real (BRL)");
                picker.Items.Add("Euro (€)");
            }
            }
            else
            {
                string cur = Preferences.Get("cur","");
                if (lang == "pt")
                {
                    if(cur == "$")
                    {
                        picker.Title = "Dólar Americano (U$)";
                    }
                    else if (cur == "R$")
                    {
                        picker.Title = "Real Brasileiro (BRL)";
                    }
                    else
                    {
                        picker.Title = "Euro (€)";
                    }
                    picker.Items.Add("Dólar Americano (U$)");
                    picker.Items.Add("Real Brasileiro (BRL)");
                    picker.Items.Add("Euro (€)");
                }
                else
                {
                    if (cur == "$")
                    {
                        picker.Title = "US Dollar (U$)";
                    }
                    else if (cur == "R$")
                    {
                        picker.Title = "Brazilian Real (BRL)";
                    }
                    else
                    {
                        picker.Title = "Euro (€)";
                    }
                    picker.Items.Add("US Dollar (U$)");
                    picker.Items.Add("Brazilian Real (BRL)");
                    picker.Items.Add("Euro (€)");
                }
            }
        }

        async override protected void OnAppearing()
        {
            base.OnAppearing();
            //CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-5325371552579828/1330879804");
            await GetPrices();
            base.OnAppearing();
        }

        public async Task<binancePrice> GetPrices()
        {
                string connectionUrl = "https://www.mercadobitcoin.net/api/BTC/ticker/";
                object userInfos = new { };
                var jsonObj = JsonConvert.SerializeObject(userInfos);
            await DisplayAlert("1",Convert.ToString(jsonObj),"ok");
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                await DisplayAlert("2", Convert.ToString(content), "ok");
                await DisplayAlert("3", Convert.ToString(connectionUrl), "ok");
                var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(connectionUrl),
                        Method = HttpMethod.Post,
                        Content = content
                    };
                    var response = await client.SendAsync(request);
                await DisplayAlert("4", Convert.ToString(response), "ok");
                string dataResult = response.Content.ReadAsStringAsync().Result;
                await DisplayAlert("5", Convert.ToString(dataResult), "ok");
                binancePrice result = JsonConvert.DeserializeObject<binancePrice>(dataResult);
                await DisplayAlert("6", Convert.ToString(result), "ok");
                string price = JObject.Parse(dataResult)["price"].ToString();
                    btcPrice.Text = price;
                    return result;
                }  
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

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currency = Convert.ToString(picker.Items[picker.SelectedIndex]);
            string lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            if (lang == "pt")
            {
                if (currency == "Dólar Americano (U$)")
                {
                    currency = "$";
                    Preferences.Set("cur","$");
                }
                else if (currency == "Real Brasileiro (BRL)")
                {
                    currency = "R$";
                    Preferences.Set("cur", "R$");
                }
                else
                {
                    currency = "€";
                    Preferences.Set("cur", "€");
                }
            }
            else
            {
                if (currency == "US Dollar (U$)")
                {
                    currency = "$";
                    Preferences.Set("cur", "$");
                }
                else if (currency == "Brazilian Real (BRL)")
                {
                    currency = "R$";
                    Preferences.Set("cur", "R$");
                }
                else
                {
                    currency = "€";
                    Preferences.Set("cur", "€");
                }
            }
        }
    }
}