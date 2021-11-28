using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table(nameof(Validation))]
public class Validation : BaseEntity
{
    [Required] public Guid UserGuid { get; set; }

    [Required] public Order Order { get; set; }

    [Required] public bool Accepted { get; set; }

    public Validation( Guid userGuid, Order order, bool accepted )
    {
        UserGuid = userGuid;
        Order    = order;
        Accepted = accepted;
    }
}