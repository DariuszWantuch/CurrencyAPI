using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CurrencyAPI.Models
{
    public class Publication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("publication_date")]
        public DateTime PublicationDate { get; set; }

        // One-to-many relationship with ExchangeRate
        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
    }
}
