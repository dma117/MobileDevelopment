using System;
using WeatherApp.Models;
using WeatherApp.Service;
using Xamarin.Forms;
using WeatherApp.Helpers;
using System.Threading.Tasks;

namespace WeatherApp.ViewModels
{
    public class WeatherPageViewModel : BaseViewModel
    {
        private WeatherInfo _weatherInfo;
        private ForecastInfo _forecastInfo;

        public WeatherPageViewModel()
        {
            SetWeatherInfoAsync();
        }

        public INavigation Navigation { get; set; }

        public async void SetWeatherInfoAsync() 
        {
            _weatherInfo = await Saver.Instance.GetCurrentWeather();

           // _weatherInfo = await HttpRequestHandler.GetModelAsync("Vladivostok");
            /*// _weatherInfo.location = "Vladivostok";
            _weatherInfo.date = DateTime.UtcNow.AddSeconds(_weatherInfo.timezone).ToString("d");

            Saver.Instance.SerializeCurrentWeather(_weatherInfo);*/


            if (HttpRequestHandler.InternetConnected())
            {
                SetWeatherByInternetConnection(LocationName);
            }
            else
            {
                ErrorMessenger.Instance.LoadWeatherFailed();
            }

            UpdateProperties();
        }

        private async void SetWeatherByInternetConnection(string location)
        {
            _weatherInfo = await HttpRequestHandler.GetModelAsync(location);
            // _weatherInfo.location = "Vladivostok";
            _weatherInfo.date = DateTime.UtcNow.AddSeconds(_weatherInfo.timezone).ToString("d");
            Saver.Instance.SerializeCurrentWeather(_weatherInfo);
        }

        private void UpdateProperties()
        {
            Temperature = (int)_weatherInfo.main.temp;
            WindSpeed = _weatherInfo.wind.speed;
            Humidity = _weatherInfo.main.humidity;
            WeatherDescription = _weatherInfo.weather[0].description;
            Date = _weatherInfo.date;
            LocationName = _weatherInfo.name;
        }

        public bool ChangeCurrentLocation(string location)
        {
            if (!HttpRequestHandler.InternetConnected())
            {
                ErrorMessenger.Instance.ChangeLocationFailed();
                
                return false;
            }

            SetWeatherByInternetConnection(location);
            UpdateProperties();
            //_weatherInfo.location = location;

            return true;
        }

        public int Temperature
        {
            get => (int) (_weatherInfo?.main?.temp ?? 0);
            
            set
            {
                _weatherInfo.main.temp = value;
                OnPropertyChanged();
            }
        }

        public double WindSpeed
        {
            get => (double)(_weatherInfo?.wind?.speed ?? 0);

            set
            {
                _weatherInfo.wind.speed = value;
                OnPropertyChanged();
            }
        }

        public int Humidity
        {
            get => (int)(_weatherInfo?.main?.humidity ?? 0);

            set
            {
                _weatherInfo.main.humidity = value;
                OnPropertyChanged();
            }
        }

        public string WeatherDescription
        {
            get => _weatherInfo?.weather?[0].description ?? String.Empty;

            set
            {
                _weatherInfo.weather[0].description = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get => _weatherInfo?.date ?? String.Empty;

            set
            {
                _weatherInfo.date = value;
                OnPropertyChanged();
            }
        }

        public string LocationName
        {
            get => _weatherInfo?.name ?? String.Empty;

            set
            {
                _weatherInfo.name = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get => _weatherInfo?.sys?.country ?? String.Empty;

            set
            {
                _weatherInfo.sys.country = value;
                OnPropertyChanged();
            }
        }
    }
}
