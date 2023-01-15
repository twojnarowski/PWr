using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string UserId { get; set; }
        public string BoughtCurrencyId { get; set; }
        public decimal BoughtCurrencyQuantity { get; set; }
        public string SoldCurrencyId { get; set; }
        public decimal SoldCurrencyQuantity { get; set; }
    }
}