using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class ValidationDto
{
    public ValidationDto( Guid id, Guid userGuid, OrderDto order, bool accepted )
    {
        Id       = id;
        UserGuid = userGuid;
        Order    = order;
        Accepted = accepted;
    }

    public Guid Id { get; set; }

    public Guid UserGuid { get; set; }

    public OrderDto Order { get; set; }

    public bool Accepted { get; set; }
}