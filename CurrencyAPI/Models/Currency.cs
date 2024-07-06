using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CurrencyAPI.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("currency_code")]
        [MaxLength(3)]
        public string CurrencyCode { get; set; }

        [Column("currency_name")]
        [MaxLength(255)]
        public string CurrencyName { get; set; }

        // One-to-many relationship with ExchangeRate
        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
    }
}
