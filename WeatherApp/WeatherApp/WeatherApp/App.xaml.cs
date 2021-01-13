using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Service;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new RootPage();
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
