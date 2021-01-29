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
        private LocationsViewModel _locationsViewModel;
        public SidePageViewModel(WeatherPageViewModel weatherPageViewModel)
        {
            _weatherPageViewModel = weatherPageViewModel;
            _locationsViewModel = new LocationsViewModel();

            ChooseTheCityCommand = new Command(ChooseTheCity);
            WeatherCommand = new Command(ShowWeather);
        }

        public ICommand ChooseTheCityCommand { get; private set; }
        public ICommand WeatherCommand { get; private set; }

        public MasterDetailPage MasterDetailPage { get; set; }

        private void ChooseTheCity()
        {
            _locationsViewModel.Text = String.Empty;
            MasterDetailPage.Detail = new NavigationPage(new LocationsView(_locationsViewModel));
        }

        private void ShowWeather()
        {
            if (_locationsViewModel.ChosenLocation != null)
            {
                _weatherPageViewModel.LocationName = _locationsViewModel.ChosenLocation;
                _weatherPageViewModel.SetWeatherInfoAsync();
            }

            MasterDetailPage.Detail = new NavigationPage(new WeatherPageView(_weatherPageViewModel));
        }
    }
}
