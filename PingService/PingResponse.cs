using Newtonsoft.Json;

namespace PingService
{
    public class PingResponse
    {
        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("roundtrip_time")]
        public long RoundtripTime { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }
}
