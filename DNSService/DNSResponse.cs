using Newtonsoft.Json;

namespace DNSService
{
    public class DNSResponse
    {
        [JsonProperty("addresses")]
        public string[]? Addresses { get; set; }

        [JsonProperty("aliases")]
        public string[]? Aliases { get; set; }
    }
}
