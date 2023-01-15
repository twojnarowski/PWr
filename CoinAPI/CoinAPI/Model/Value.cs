using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinAPI.Model
{
    public class Value
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ValueID { get; set; }

        public string CurrencyID { get; set; }
        public DateTime? Timestamp { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal Rate { get; set; }
    }
}