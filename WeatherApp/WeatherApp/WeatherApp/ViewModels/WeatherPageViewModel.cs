using System;
using WeatherApp.Models;
using WeatherApp.Service;

namespace WeatherApp.ViewModels
{
    public class WeatherInfoViewModel : BaseViewModel
    {
        private WeatherInfo _weatherInfo;

        public WeatherInfoViewModel()
        {
            SetWeatherInfoAsync();
        }

        public async void SetWeatherInfoAsync() 
        {
            _weatherInfo = await HttpRequestHandler.GetModelAsync();

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
    }
}
