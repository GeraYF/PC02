using Newtonsoft.Json;

namespace examen.Models
{
    public class CoinGeckoResponse
    {
        [JsonProperty("bitcoin")]
        public BitcoinResponse? Bitcoin { get; set; }
    }

    public class BitcoinResponse
    {
        [JsonProperty("usd")]
        public UsdResponse? Usd { get; set; }
    }

    public class UsdResponse
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
}
