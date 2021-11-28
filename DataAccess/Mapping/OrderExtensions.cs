using DataAccess.Entities;
using Models.DTO;

namespace DataAccess.Mapping;

public static class OrderExtensions
{
    public static OrderDto AsDto( this Order order )
    {
        return new OrderDto
        {
            UserGuid      = order.UserGuid,
            WalletAddress = order.WalletAddress,
            Network       = order.Network,
            Amount        = order.Amount,
            Coin          = order.Coin.AsDto(),
            Status        = order.Status.ToString()
        };
    }
}