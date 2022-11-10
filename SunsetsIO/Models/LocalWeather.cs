using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SunsetsIO.Models
{
    public class LocalWeather : IDisposable
    {
        public LocalWeather() 
        {
            DateTimeUtc = DateTime.UtcNow;
        }
        
        public int Id { get; set; }
        [Precision(4, 1)]
        public double Latitude { get; set; }
        [Precision(4, 1)]
        public double Longitude { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public DateTime SunsetUtc { get; set; }
        public JsonDocument Payload { get; set; }

        public void Dispose() => Payload?.Dispose();
    }
}