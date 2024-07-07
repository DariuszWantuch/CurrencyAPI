using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
namespace CurrencyAPI.Models.DTO
{
    public class ExchangeRateDTO
    {
        [JsonPropertyName("effectiveDate")]
        public DateTime PublicationDate { get; set; }
        [JsonPropertyName("rates")]
        public ICollection<CurrencyDTO> CurrencyDTOs { get; set; }
    }
}

