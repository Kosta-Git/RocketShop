using Binance.Net.Objects.Models.Spot;
using Models.Results;

namespace DataAccess.Services;

public interface ICoinService
{
    Task<Result<IEnumerable<BinanceUserAsset>>> GetCoins();

    Task<Result<BinanceUserAsset>> GetCoin( string coin );

    Task<Result<IEnumerable<BinanceNetwork>>> GetCoinNetworks( string coin );
}