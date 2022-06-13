using Microsoft.Toolkit.Parsers.Rss;
using RSSExemplo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RSSExemplo.ViewModels
{
    class TwitterViewModel
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

        public TwitterViewModel()
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

            using (var client = new HttpClient())
            {

                feed = await client.GetStringAsync("https://rss.app/feeds/zJsTzWULNvmNEtW4.xml");
            }
            //ini feed
            if (feed != null)
            {
                RSSFeed.Clear();

                var parser = new RssParser();
                var rss = parser.Parse(feed).OrderByDescending(e => e.PublishDate).ToList(); ;


                foreach (var rssSchema in rss)
                {
                    RSSFeed.Add(new RssData
                    {
                        Title = "@"+rssSchema.Title,
                        PubDate = rssSchema.PublishDate,
                        Link = rssSchema.FeedUrl,
                        Guid = rssSchema.InternalID,
                        Author = "@"+rssSchema.Author,
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
