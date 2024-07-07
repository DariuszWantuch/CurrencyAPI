using System.Text.Json.Serialization;

namespace CurrencyAPI.Models.DTO
{
    public class RateDTO
    {
        public int Id { get; set; }
        public string PublicationDate { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal Rate { get; set; }

    }
}
