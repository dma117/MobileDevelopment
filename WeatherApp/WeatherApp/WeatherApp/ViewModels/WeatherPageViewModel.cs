using System;
using WeatherApp.Models;
using WeatherApp.Service;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherPageViewModel : BaseViewModel
    {
        private WeatherInfo _weatherInfo;
        private string _location;

        public WeatherPageViewModel()
        {
            _location = "Vladivostok";

            SetWeatherInfoAsync();
        }

        public INavigation Navigation { get; set; }

        public async void SetWeatherInfoAsync() 
        {
            _weatherInfo = await HttpRequestHandler.GetModelAsync(LocationName);

            Temperature = (int)_weatherInfo.main.temp;
            WindSpeed = _weatherInfo.wind.speed;
            Humidity = _weatherInfo.main.humidity;
            WeatherDescription = _weatherInfo.weather[0].description;
            Date = DateTime.UtcNow.AddSeconds(_weatherInfo.timezone).ToString("d");
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
            get => _location;

            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }
    }
}
