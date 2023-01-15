using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string UserId { get; set; }
        public string CurrencyID { get; set; }
        public decimal Quantity { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}