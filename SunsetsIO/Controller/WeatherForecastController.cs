using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SunsetsIO.Models;
namespace SunsetsIO.Controllers
{
    public class WeatherForecastController
    {
        public async Task<OpenWeatherMapPayload> GetWeatherForecastAsync(double latitude, double longitude)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={Environment.GetEnvironmentVariable("OPEN_WEATHER_MAP_API_KEY")}");
            var responseString = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<OpenWeatherMapPayload>(responseString);
            return weatherForecast;
        }
    }
}
