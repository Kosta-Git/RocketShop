using DataAccess.Entities;
using Models.DTO;

namespace DataAccess.Mapping;

public static class CoinExtensions
{
    public static CoinDto AsDto( this Coin coin )
    {
        return new CoinDto(coin.Id, coin.Name, coin.Identifier);
    }

    public static Coin AsCoin( this CoinCreateDto coin )
    {
        return new Coin(coin.Name, coin.Identifier, null);
    }
}