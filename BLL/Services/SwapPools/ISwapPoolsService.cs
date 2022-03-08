using Binance.Net.Objects.Models.Spot.BSwap;
using Models.Results;

namespace BLL.Services.SwapPools;

public interface ISwapPoolsService
{
    Task<Result<IEnumerable<BinanceBSwapPool>>> GetAll();

    Task<Result> Exists( string primaryAsset, string secondaryAsset );
}