using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Parsers.Rss;
using RSSExemplo.Models;
using Xamarin.Forms;

namespace RSSExemplo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Property

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ObservableCollection<RssData> RSSFeed { get; }

        public MainViewModel()
        {
            RSSFeed = new ObservableCollection<RssData>();
            CarregaRSS();
        }

        private ICommand _refreshNewsFeedCommand;

        public ICommand RefreshNewsFeedCommand =>
            _refreshNewsFeedCommand ?? (_refreshNewsFeedCommand = new Command(
                async () =>
                {
                    await CarregaRSS();

                }));



        private async Task CarregaRSS()
        {
            IsBusy = true;

            string feed = null;
            string feed2 = null;
            string feed3 = null;
            string feed4 = null;

            using (var client = new HttpClient())
            {
                string lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

                    if (lang == "pt")
                    {
                    feed = await client.GetStringAsync("https://livecoins.com.br/feed/");
                    feed2 = await client.GetStringAsync("https://cointelegraph.com.br/rss");
                    feed3 = await client.GetStringAsync("https://portaldobitcoin.uol.com.br/feed/");
                    feed4 = await client.GetStringAsync("https://www.criptofacil.com/feed/");
                    }
                    else
                    {
                    feed = await client.GetStringAsync("https://news.bitcoin.com/feed/");
                    feed2 = await client.GetStringAsync("https://cointelegraph.com/rss");
                    feed3 = await client.GetStringAsync("https://bitcoinmagazine.com/.rss/full/");
                    feed4 = await client.GetStringAsync("https://cryptonews.com/news/feed");
                }
               
            }
            //ini feed
            if (feed != null)
            {
                //RSSFeed.Clear();

                var parser = new RssParser();
                var rss = parser.Parse(feed).OrderByDescending(e => e.PublishDate).ToList(); ;


                foreach (var rssSchema in rss)
                {
                    RSSFeed.Add(new RssData
                    {
                        Title = rssSchema.Title,
                        PubDate = rssSchema.PublishDate,
                        Link = rssSchema.FeedUrl,
                        Guid = rssSchema.InternalID,
                        Author = rssSchema.Author,
                        Thumbnail = string.IsNullOrWhiteSpace(rssSchema.ImageUrl) ? $"" : rssSchema.ImageUrl,
                        Description = rssSchema.Summary
                    });
                }
                IsBusy = false;

            }
            //fim feed
            //ini feed
            if (feed2 != null)
            {
                //RSSFeed.Clear();

                var parser = new RssParser();
                var rss = parser.Parse(feed2).OrderByDescending(e => e.PublishDate).ToList(); ;


                foreach (var rssSchema in rss)
                {
                    RSSFeed.Add(new RssData
                    {
                        Title = rssSchema.Title,
                        PubDate = rssSchema.PublishDate,
                        Link = rssSchema.FeedUrl,
                        Guid = rssSchema.InternalID,
                        Author = rssSchema.Author,
                        Thumbnail = string.IsNullOrWhiteSpace(rssSchema.ImageUrl) ? $"" : rssSchema.ImageUrl,
                        Description = rssSchema.Summary
                    });
                }
                IsBusy = false;

            }
            //fim feed
            //ini feed
            if (feed3 != null)
            {
                //RSSFeed.Clear();

                var parser = new RssParser();
                var rss = parser.Parse(feed3).OrderByDescending(e => e.PublishDate).ToList(); ;


                foreach (var rssSchema in rss)
                {
                    RSSFeed.Add(new RssData
                    {
                        Title = rssSchema.Title,
                        PubDate = rssSchema.PublishDate,
                        Link = rssSchema.FeedUrl,
                        Guid = rssSchema.InternalID,
                        Author = rssSchema.Author,
                        Thumbnail = string.IsNullOrWhiteSpace(rssSchema.ImageUrl) ? $"" : rssSchema.ImageUrl,
                        Description = rssSchema.Summary
                    });
                }
                IsBusy = false;

            }
            //fim feed
            //ini feed
            if (feed4 != null)
            {
                //RSSFeed.Clear();

                var parser = new RssParser();
                var rss = parser.Parse(feed4).OrderByDescending(e => e.PublishDate).ToList(); ;


                foreach (var rssSchema in rss)
                {
                    RSSFeed.Add(new RssData
                    {
                        Title = rssSchema.Title,
                        PubDate = rssSchema.PublishDate,
                        Link = rssSchema.FeedUrl,
                        Guid = rssSchema.InternalID,
                        Author = rssSchema.Author,
                        Thumbnail = string.IsNullOrWhiteSpace(rssSchema.ImageUrl) ? $"" : rssSchema.ImageUrl,
                        Description = rssSchema.Summary
                    });
                }
                IsBusy = false;

            }
            //fim feed
        }
    }
}
