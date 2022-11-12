using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SunsetsIO.Data;
using SunsetsIO.Models;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SunsetsIO.Controllers
{
    public class WeatherForecastController
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public WeatherForecastController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<LocalWeather> GetLocalWeather(double latitude, double longitude)
        {
            double reducedLat = Math.Round(latitude, 1);
            double reducedLong = Math.Round(longitude, 1);

            // TODO: Add time to query
            LocalWeather? localWeather = await _context.LocalWeather
                    .Where(lw => lw.Latitude == reducedLat && lw.Longitude == reducedLong)
                    .OrderByDescending(lw => lw.SunsetUtc)
                    .FirstOrDefaultAsync();
            
            bool localWeatherExists = false;
            if (localWeather is not null)
            {
                var localWeatherSunsetOffset = localWeather.SunsetUtc.AddSeconds(localWeather.TimezoneOffsetSecs);
                var localTimeOffset = DateTime.UtcNow.AddSeconds(localWeather.TimezoneOffsetSecs);
                
                if (localWeatherSunsetOffset.Date == localTimeOffset.Date)
                {
                    localWeatherExists = true;
                }
            }
            
            if (localWeather is null || !localWeatherExists) // if it doesn't exist
            {
                var localWeatherData = await GetLocalWeatherAsync(reducedLat, reducedLong);

                int sunsetUtcEpoch = localWeatherData.RootElement.GetProperty("city").GetProperty("sunset").GetInt32();
                DateTime sunsetUtc = DateTimeOffset.FromUnixTimeSeconds(sunsetUtcEpoch).UtcDateTime;
                
                int timezoneOffsetSecs = localWeatherData.RootElement.GetProperty("city").GetProperty("timezone").GetInt32();

                localWeather = new LocalWeather()
                {
                    Latitude = reducedLat,
                    Longitude = reducedLong,
                    Payload = localWeatherData,
                    SunsetUtc = sunsetUtc,
                    TimezoneOffsetSecs = timezoneOffsetSecs
                };
                
                _context.LocalWeather.Add(localWeather);
                await _context.SaveChangesAsync();
            }

            return localWeather;
        }

        public async Task<JsonDocument> GetLocalWeatherAsync(double latitude, double longitude)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={_config["OPEN_WEATHER_MAP_API_KEY"]}");
            var responseString = response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            var responseJson = response.Content.ReadAsStringAsync().Result;
            // TODO: Put into Try-Catch block
            var weatherForecast = JsonDocument.Parse(responseJson);
            return weatherForecast;
        }

        public class LocalWeatherViewModel
        {
            public int LocalWeatherId { get; set; }
            public DateTime SunsetUtc { get; set; }
        }
    }
}
