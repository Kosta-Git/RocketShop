using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class ValidationCreateDto
{
    public ValidationCreateDto( Guid userGuid, Guid orderId, bool accepted )
    {
        UserGuid = userGuid;
        OrderId  = orderId;
        Accepted = accepted;
    }

    [Required] public Guid UserGuid { get; set; }

    [Required] public Guid OrderId { get; set; }

    [Required] public bool Accepted { get; set; }
}