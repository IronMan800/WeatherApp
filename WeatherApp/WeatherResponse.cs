using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
        public string Name { get; set; }
    }
}
