using Binance.Net.Objects.Models.Spot.BSwap;
using Models.Results;

namespace BLL.Services.Swaps;

public interface ISwapService
{
    Task<Result<BinanceBSwapQuote>> GetQuote( string from, string to, decimal quantity );
    Task<Result<BinanceBSwapResult>> Swap( string from, string to, decimal quantity );
}