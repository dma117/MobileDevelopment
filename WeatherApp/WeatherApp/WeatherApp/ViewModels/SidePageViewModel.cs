using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WeatherApp.Views;
using Xamarin.Forms;
using WeatherApp.ViewModels;
using WeatherApp.Service;
using System.Threading.Tasks;

namespace WeatherApp.ViewModels
{
    public class SidePageViewModel
    {
        private WeatherPageViewModel _weatherPageViewModel;
        private HistoryLocationsViewModel _historyLocationsViewModel;
        public SidePageViewModel(WeatherPageViewModel weatherPageViewModel)
        {
            _weatherPageViewModel = weatherPageViewModel;
            _historyLocationsViewModel = new HistoryLocationsViewModel();

            Task.Run(() => {
                LocationsLoader.Instance.LoadLocations();
            });

            ChooseTheCityCommand = new Command(ChooseTheCity);
            WeatherCommand = new Command(ShowWeather);
        }

        public ICommand ChooseTheCityCommand { get; private set; }
        public ICommand WeatherCommand { get; private set; }

        public MasterDetailPage MasterDetailPage { get; set; }

        private void ChooseTheCity()
        {
            MasterDetailPage.Detail = new NavigationPage(new HistoryView(_historyLocationsViewModel));
        }

        private void ShowWeather()
        {
            if (_historyLocationsViewModel.ChosenLocation != null)
            {
                _weatherPageViewModel.LocationName = _historyLocationsViewModel.ChosenLocation;
                _weatherPageViewModel.SetWeatherInfoAsync();
            }

            MasterDetailPage.Detail = new NavigationPage(new WeatherPageView(_weatherPageViewModel));
        }
    }
}
