using System.Text.Json.Serialization;

namespace CurrencyAPI.Models.DTO
{
    public class CurrencyDTO
    {
        [JsonPropertyName("code")]
        public string CurrencyCode { get; set; }
        [JsonPropertyName("currency")]
        public string CurrencyName { get; set; }
        [JsonPropertyName("mid")]
        public decimal Rate { get; set; }
    }
}
