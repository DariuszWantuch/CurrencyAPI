using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CurrencyAPI.Models
{
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("currency_id")]
        public int CurrencyId { get; set; }

        [Required]
        [Column("publication_id")]
        public int PublicationId { get; set; }

        [Required]
        [Column("rate")]
        [Precision(18,4)]
        public decimal Rate { get; set; }

        // Many-to-one relationship with Currency
        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }

        // Many-to-one relationship with Publication
        [ForeignKey("PublicationId")]
        public virtual Publication Publication { get; set; }
    }
}
