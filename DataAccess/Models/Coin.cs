using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table(nameof(Coin))]
    public class Coin : BaseEntity
    {
        [Required] [MaxLength( 256 )] public string Name { get; set; }

        [Required] [MaxLength( 128 )] public string Identifier { get; set; }

        public List<Order> Orders { get; set; }
    }
}