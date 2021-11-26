using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Order : BaseEntity
    {
        [Required] public Guid UserGuid { get; set; }

        [Required] [MaxLength( 512 )] public string WalletAddress { get; set; }

        [Required] [MaxLength( 64 )] public string Network { get; set; }

        [Required] public float Amount { get; set; }

        [Required] public Coin Coin { get; set; }

        [Required] public Status Status { get; set; }

        [Required] public ValidationRule ValidationRule { get; set; }
    }
}