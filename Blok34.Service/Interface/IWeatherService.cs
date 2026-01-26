using Blok34.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Service.Interface
{
    public interface IWeatherService
    {
        List<Weather> GetDailyWeather(double latitude, double longitude);
    }
}
