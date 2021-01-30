using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public class Saver
    {
        private static Saver _instance;
        private string _pathHistory;
        private string _pathWeather;


        private Saver()
        {
            _pathHistory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "history.json");
            _pathWeather = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "weather.json");
        }

        public static Saver Instance
        {
            get => _instance == null ? _instance = new Saver() : _instance;
        }

        public async void SerializeHistory(IEnumerable<string> list)
        {

            using (var sw = new StreamWriter(_pathHistory))
            {
                string task = await Task.Run(() =>
                 (JsonConvert.SerializeObject(list)));

                sw.Write(task);
            }
        }

        public async void SerializeCurrentWeather(WeatherInfo info)
        {
            using (var sw = new StreamWriter(_pathWeather))
            {
                await Task.Run(() =>
                {
                    var str = JsonConvert.SerializeObject(info);
                    sw.Write(str);
                });
            }
        }

        public IEnumerable<string> GetHistory()
        {
            using (var sr = new StreamReader(_pathHistory))
            {
                return JsonConvert.DeserializeObject<IEnumerable<string>>(sr.ReadToEnd());
            }
        }

        public async Task<WeatherInfo> GetCurrentWeather()
        {
            using (var sr = new StreamReader(_pathWeather))
            {
                var str = await sr.ReadToEndAsync();
                //var elements = JsonConvert.DeserializeObject<WeatherInfo>(sr.ReadLine());
                return JsonConvert.DeserializeObject<WeatherInfo>(str);
            }
        }
    }
}
