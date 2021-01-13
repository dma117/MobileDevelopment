using System;
using System.Collections.Generic;
using System.Text;
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
        }

        public int Temperature
        {
            get => (int)_weatherInfo.main.temp;

            set
            {
                if (value != _weatherInfo.main.temp)
                {
                    _weatherInfo.main.temp = value;
                    OnPropertyChanged();
                }
            }
        }

        public double WindSpeed
        {
            get => _weatherInfo.wind.speed;

            set
            {
                if (value != _weatherInfo.wind.speed)
                {
                    _weatherInfo.wind.speed = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Humidity
        {
            get => _weatherInfo.main.humidity;

            set
            {
                if (value != _weatherInfo.main.humidity)
                {
                    _weatherInfo.main.humidity = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WeatherDescription
        {
            get => _weatherInfo.weather[0].description;

            set
            {
                if (value != _weatherInfo.weather[0].description)
                {
                    _weatherInfo.weather[0].description = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
