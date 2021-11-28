using DataAccess.Entities;
using Models.DTO;

namespace DataAccess.Mapping;

public static class OrderExtensions
{
    public static OrderDto AsDto( this Order order )
    {
        return new OrderDto(
            order.UserGuid,
            order.WalletAddress,
            order.Network,
            order.Amount,
            order.Coin.AsDto(),
            order.Status.ToString()
        );
    }
}