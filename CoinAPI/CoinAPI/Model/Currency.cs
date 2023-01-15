using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinAPI.Model
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CurrencyID { get; set; }

        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}