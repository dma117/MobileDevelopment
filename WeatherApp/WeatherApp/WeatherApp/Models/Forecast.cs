using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class Forecast
    {
        public string Day { get; set; }
        public string Date { get; set; }
        public string Icon { get; set; }
        public int Temp { get; set; }
    }
}
