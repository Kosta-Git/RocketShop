using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using Models.DTO;
using Models.Enums;

namespace DataAccess.Mapping;

public static class OrderExtensions
{
    public static OrderDto AsDto( this Order order )
    {
        return new OrderDto(
            order.Id,
            order.UserGuid,
            order.WalletAddress,
            order.WalletAddressTag,
            order.Network,
            order.Amount,
            order.Coin,
            order.Status.ToString(),
            order.ValidationRule.AsDto(),
            order.Validations?.Select( v => v.AsDto() )?.ToList() ?? new List<ValidationDto>( 0 )
        );
    }

    public static Order AsEntity( this OrderCreateDto order )
    {
        return new Order
        {
            UserGuid         = order.UserGuid,
            WalletAddress    = order.WalletAddress,
            WalletAddressTag = order.WalletAddressTag,
            Network          = order.Network,
            Amount           = order.Amount,
            Coin             = order.Coin,
            Status           = Status.Pending
        };
    }
}