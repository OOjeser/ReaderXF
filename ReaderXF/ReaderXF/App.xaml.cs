﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReaderXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainNavigationPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
