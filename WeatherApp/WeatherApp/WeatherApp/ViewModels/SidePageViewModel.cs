using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WeatherApp.Views;
using Xamarin.Forms;
using WeatherApp.ViewModels;

namespace WeatherApp.ViewModels
{
    public class SidePageViewModel
    {
        private WeatherPageViewModel _weatherPageViewModel;
        public SidePageViewModel(WeatherPageViewModel weatherPageViewModel)
        {
            _weatherPageViewModel = weatherPageViewModel;

            ChooseTheCityCommand = new Command(ChooseTheCity);
            WeatherCommand = new Command(ShowWeather);
        }

        public ICommand ChooseTheCityCommand { get; private set; }
        public ICommand WeatherCommand { get; private set; }

        public MasterDetailPage MasterDetailPage { get; set; }

        private void ChooseTheCity()
        {
            MasterDetailPage.Detail = new NavigationPage(new LocationsView(new LocationsViewModel()));
        }

        private void ShowWeather()
        {
            MasterDetailPage.Detail = new NavigationPage(new WeatherPageView(_weatherPageViewModel));
        }
    }
}
