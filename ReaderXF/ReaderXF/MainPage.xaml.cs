using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReaderXF
{

    public class book
    {
        public UriImageSource Image { get => new UriImageSource() { Uri = new Uri(imageUrl), CacheValidity = new TimeSpan(1, 0, 0, 0) }; }
        public string name { get; set; }
        public string id { get; set; }
        public string imageUrl { get; set; }
        public Command Open { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        public ObservableCollection<book> rec = new ObservableCollection<book>();
        public ObservableCollection<book> books = new ObservableCollection<book>();
        public MainPage()
        {
            InitializeComponent();
            LoadRec();
            LoadAll();

            Device.StartTimer(new TimeSpan(0,0,6), () => 
            {
                try
                {
                    carusel.CurrentItem = rec[(rec.IndexOf(carusel.CurrentItem as book) + 1) % rec.Count];
                }
                catch { }
                return true;
            });

        }

        public async void LoadRec()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://readerservercore.azurewebsites.net/api/get/books?name=recomend");
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    rec = JsonConvert.DeserializeObject<ObservableCollection<book>>(reader.ReadToEnd());
                    foreach (var b in rec)
                    {
                        b.Open = new Command(x => Navigation.PushAsync(new ReadPage(b.id, mode.SelectedIndex)));
                    }
                }
            }
            response.Close();
            carusel.ItemsSource = rec;
            loadingRec.IsVisible = false;
        }
        public async void LoadAll()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://readerservercore.azurewebsites.net/api/get/books");
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    books = JsonConvert.DeserializeObject<ObservableCollection<book>>(reader.ReadToEnd());
                    foreach (var b in books)
                    {
                        b.Open = new Command(x => Navigation.PushAsync(new ReadPage(b.id, mode.SelectedIndex)));
                    }
                }
            }
            response.Close();
            BooksList.ItemsSource = books;
            loadingAll.IsVisible = false;
        }

        private async void BooksList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new ReadPage((e.SelectedItem as book).id, mode.SelectedIndex));
        }
    }
}
