using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Validation : BaseEntity
{
    [Required] public Guid UserGuid { get; set; }

    [Required] public Order Order { get; set; }

    [Required] public bool Accepted { get; set; }
}