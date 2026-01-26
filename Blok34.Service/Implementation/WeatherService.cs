using Blok34.Domain.DomainModels;
using Blok34.Domain.DTO;
using Blok34.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blok34.Service.Implementation
{
    public class WeatherService : IWeatherService
    {
        public List<Weather> GetDailyWeather(double latitude, double longitude)
        {
            var url =
                $"https://api.open-meteo.com/v1/forecast" +
                $"?latitude={latitude}&longitude={longitude}" +
                $"&daily=temperature_2m_max,temperature_2m_min,apparent_temperature_max," +
                $"precipitation_sum,precipitation_hours,precipitation_probability_max," +
                $"wind_speed_10m_max,wind_gusts_10m_max,uv_index_max" +
                $"&timezone=auto";

            using var client = new HttpClient();
            var response = client.GetStringAsync(url).Result;

            var weatherDTO = JsonSerializer.Deserialize<WeatherDTO>(response);

            var result = new List<Weather>();

            for (int i = 0; i < weatherDTO?.daily.time.Count; i++)
            {
                result.Add(new Weather
                {
                    Date = DateTime.Parse(weatherDTO.daily.time[i]),
                    MaxTemp = weatherDTO.daily.temperature_2m_max[i],
                    MinTemp = weatherDTO.daily.temperature_2m_min[i],
                    FeelsLike = weatherDTO.daily.apparent_temperature_max[i],
                    RainMm = weatherDTO.daily.precipitation_sum[i],
                    RainHours = weatherDTO.daily.precipitation_hours[i],
                    RainProbability = weatherDTO.daily.precipitation_probability_max[i],
                    WindSpeed = weatherDTO.daily.wind_speed_10m_max[i],
                    WindGusts = weatherDTO.daily.wind_gusts_10m_max[i],
                    UvIndex = weatherDTO.daily.uv_index_max[i]
                });
            }

            return result;
        }
    }
}
