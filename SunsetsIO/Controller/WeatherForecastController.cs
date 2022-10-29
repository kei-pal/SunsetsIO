using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SunsetsIO.Controllers
{
    public class WeatherForecastController
    {
        public async Task<WeatherForecastResponse> GetWeatherForecastAsync(double latitude, double longitude)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={Environment.GetEnvironmentVariable("OPEN_WEATHER_MAP_API_KEY")}");
            var responseString = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<WeatherForecastResponse>(responseString);
            return weatherForecast;
        }
        
        public class WeatherForecastResponse
        {
            public string cod { get; set; }
            public double message { get; set; }
            public int cnt { get; set; }
            public List<WeatherForecast> list { get; set; }
            public City city { get; set; }
        }

        public class WeatherForecast
        {
            public int dt { get; set; }
            public Main main { get; set; }
            public List<Weather> weather { get; set; }
            public Clouds clouds { get; set; }
            public Wind wind { get; set; }
            public Rain rain { get; set; }
            public Sys sys { get; set; }
            public string dt_txt { get; set; }
        }
        
        public class Main
        {
            public float temp { get; set; }
            public float feels_like { get; set; }
            public float temp_min { get; set; }
            public float temp_max { get; set; }
            public int pressure { get; set; }
            public int sea_level { get; set; }
            public int grnd_level { get; set; }
            public int humidity { get; set; }
            public float temp_kf { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Wind
        {
            public float speed { get; set; }
            public int deg { get; set; }
            public float gust { get; set; }
        }
        
        public class Rain
        {
            public float ThreeHourly { get; set; }
        }
        
        public class Sys
        {
            public char pod { get; set; }
        }
        
        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        
        public class City
        {
            public int id { get; set; }
            public string name { get; set; }
            public Coord coord { get; set; }
            public string country { get; set; }
            public int timezone { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }
    }
}
