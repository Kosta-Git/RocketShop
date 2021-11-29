using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class BaseEntity
{
    [Key] public Guid Id { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    [Required] public DateTime LastUpdatedAt { get; set; }
}