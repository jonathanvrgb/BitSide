using MarcTron.Plugin;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RSSExemplo
{
    public partial class RSSWebViewPage : ContentPage
    {
        public RSSWebViewPage(string url)
        {
            InitializeComponent();

            webView.Source = url;
        }

        async override protected void OnAppearing()
        {
            base.OnAppearing();
            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-5325371552579828/2601532161");
            base.OnAppearing();
        }

    }
}
