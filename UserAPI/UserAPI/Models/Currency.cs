using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models
{
    public class Currency
    {
        [Key]
        public string CurrencyID { get; set; }

        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}