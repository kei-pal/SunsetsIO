using System.Text.Json;

namespace SunsetsIO.Models
{
    public class LocalWeather : IDisposable
    {
        public int Id { get; set; }
        public JsonDocument Payload { get; set; } = default!;
        public void Dispose() => Payload?.Dispose();
    }
}
