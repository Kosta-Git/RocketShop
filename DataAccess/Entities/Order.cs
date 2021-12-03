using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Enum;

namespace DataAccess.Entities;

[Table( nameof( Order ) )]
public class Order : BaseEntity
{
    [Required] public Guid UserGuid { get; set; }

    [Required] [MaxLength( 512 )] public string WalletAddress { get; set; }

    [Required] [MaxLength( 64 )] public string Network { get; set; }

    [Required] public float Amount { get; set; }

    [Required] public Coin Coin { get; set; }

    [Required] public Status Status { get; set; }

    [Required] public ValidationRule ValidationRule { get; set; }

    [Required] public List<Validation> Validations { get; set; }
}