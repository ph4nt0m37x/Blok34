using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.DTO
{
    public class WeatherDTO
    {
        public Daily daily { get; set; }
    }

    public class Daily
    {
        public List<string> time { get; set; }
        public List<double> temperature_2m_max { get; set; }
        public List<double> temperature_2m_min { get; set; }
        public List<double> apparent_temperature_max { get; set; }
        public List<double> precipitation_sum { get; set; }
        public List<double> precipitation_hours { get; set; }
        public List<int> precipitation_probability_max { get; set; }
        public List<double> wind_speed_10m_max { get; set; }
        public List<double> wind_gusts_10m_max { get; set; }
        public List<double> uv_index_max { get; set; }
    }
}
