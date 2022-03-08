using DataAccess.Entities;
using Models.DTO;

namespace DataAccess.Mapping;

public static class ValidationExtensions
{
    public static Validation AsEntity( this ValidationCreateDto validation )
    {
        return new Validation
        {
            UserGuid = validation.UserGuid,
            Order    = new Order { Id = validation.OrderId },
            Accepted = validation.Accepted
        };
    }

    public static ValidationDto AsDto( this Validation validation )
    {
        return new ValidationDto( validation.Id, validation.UserGuid, validation.Order.AsDto(), validation.Accepted );
    }
}