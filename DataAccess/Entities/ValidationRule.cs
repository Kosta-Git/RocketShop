using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table( nameof( ValidationRule ) )]
public class ValidationRule : BaseEntity
{
    [Required] public float Start { get; set; }

    [Required] public float End { get; set; }

    [Required] public uint Confirmations { get; set; }

    [Required] public bool Enabled { get; set; }
}