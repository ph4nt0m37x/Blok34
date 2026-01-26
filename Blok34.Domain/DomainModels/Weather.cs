using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Domain.DomainModels
{
    public class Weather
    {
        public DateTime Date { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double FeelsLike { get; set; }
        public double RainMm { get; set; }
        public double RainHours { get; set; }
        public int RainProbability { get; set; }
        public double WindSpeed { get; set; }
        public double WindGusts { get; set; }
        public double UvIndex { get; set; }
    }
}
