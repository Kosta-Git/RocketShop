using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Enums;

namespace DataAccess.Entities;

[Table( nameof( Order ) )]
public class Order : BaseEntity
{
    [Required] public Guid UserGuid { get; set; }

    [Required] [MaxLength( 512 )] public string WalletAddress { get; set; }

    [MaxLength( 512 )] public string WalletAddressTag { get; set; }

    [Required] public string Coin { get; set; }

    [Required] [MaxLength( 64 )] public string Network { get; set; }

    [Required] public float Amount { get; set; }

    [Required] public Status Status { get; set; }

    [Required] public ValidationRule ValidationRule { get; set; }

    [Required] public List<Validation> Validations { get; set; }
}