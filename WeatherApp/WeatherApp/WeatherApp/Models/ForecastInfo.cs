using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class ForecastInfo
    {
        public List<List> list { get; set; }
    }
    public class List
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public Weather[] weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public DateTime dt_txt { get; set; }
    }
}
