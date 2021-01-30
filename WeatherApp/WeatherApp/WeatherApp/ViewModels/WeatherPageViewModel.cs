using System;
using WeatherApp.Models;
using WeatherApp.Service;
using Xamarin.Forms;
using WeatherApp.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherApp.ViewModels
{
    public class WeatherPageViewModel : BaseViewModel
    {
        private WeatherInfo _weatherInfo;
        private List<Forecast> _forecast;
        private readonly int _days = 7;

        public WeatherPageViewModel()
        {
            _forecast = new List<Forecast>();
            SetWeatherInfoAsync();
        }

        public INavigation Navigation { get; set; }

        public async void SetWeatherInfoAsync() 
        {
            _weatherInfo = await Saver.Instance.GetCurrentWeather();

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
            _weatherInfo.date = DateTime.UtcNow.AddSeconds(_weatherInfo.timezone).ToString("d");
            Saver.Instance.SerializeCurrentWeather(_weatherInfo);
            
            var list = (await HttpRequestHandler.GetForecastInfoAsync(LocationName)).list;
            _forecast.Clear();

            for (int i = 1; i < list.Count; i++)
            {
                var date = list[i].dt_txt;

                if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                {
                    _forecast.Add(new Forecast()
                    {
                        Day = list[i].dt_txt.ToString("dddd"),
                        Date = list[i].dt_txt.ToString("dd MMM"),
                        Icon = $"https://openweathermap.org/img/wn/{ list[i].weather[0].icon}@2x.png",
                        Temp = (int)Math.Round(list[i].main.temp),
                    });
                }
            }
        }

        private void UpdateProperties()
        {
            Temperature = (int)_weatherInfo.main.temp;
            WindSpeed = _weatherInfo.wind.speed;
            Humidity = _weatherInfo.main.humidity;
            WeatherDescription = _weatherInfo.weather[0].description;
            Date = _weatherInfo.date;
            LocationName = _weatherInfo.name;
            Forecast = _forecast;
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

        public List<Forecast> Forecast
        {
            get => _forecast;

            set
            {
                _forecast = value;
                OnPropertyChanged();
            }
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
