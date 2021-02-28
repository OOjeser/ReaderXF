using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReaderXF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationPage : NavigationPage
    {
        public MainNavigationPage()
        {
            InitializeComponent();
            SetHasNavigationBar(this, false);
            Navigation.PushAsync(new MainPage());
        }
    }
}