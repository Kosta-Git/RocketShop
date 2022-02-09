using Binance.Net.Objects.Spot.WalletData;
using Models.Results;

namespace DataAccess.Services;

public interface ICoinService
{
    Task<Result<IEnumerable<BinanceUserCoin>>> GetCoins();

    Task<Result<BinanceUserCoin>> GetCoin( string coin );

    Task<Result<IEnumerable<BinanceNetwork>>> GetCoinNetworks( string coin );
}