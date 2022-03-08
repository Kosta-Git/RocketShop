using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Spot.BSwap;
using BLL.Services.Funds;
using BLL.Services.SwapPools;
using Microsoft.Extensions.Logging;
using Models.Results;

namespace BLL.Services.Swaps;

public class SwapService : ISwapService
{
    private readonly ISwapPoolsService _poolsService;
    private readonly IFundService _fundService;
    private readonly IBinanceClient _client;
    private readonly ILogger<SwapService> _logger;

    public SwapService( ISwapPoolsService poolsService, IFundService fundService, IBinanceClient client,
                        ILogger<SwapService> logger )
    {
        _poolsService = poolsService;
        _fundService  = fundService;
        _client       = client;
        _logger       = logger;
    }

    public async Task<Result<BinanceBSwapQuote>> GetQuote( string from, string to, decimal quantity )
    {
        _logger.LogDebug( "Verifying swap pool {from}:{to} exists", from, to );

        if ( ( await _poolsService.Exists( from, to ) ).Failure )
        {
            _logger.LogWarning( "Tried to swap assets with no existing pool {from}:{to}", from, to );
            return Result.Fail<BinanceBSwapQuote>( "Asset swap is not possible", ResultStatus.InvalidInput );
        }

        if ( ( await _fundService.IsAvailable( from, quantity ) ).Failure )
        {
            _logger.LogCritical( "Not enough funds for swap {from}:{to} amount: {quantity}", from, to, quantity );
            return Result.Fail<BinanceBSwapQuote>( "Asset swap is not possible", ResultStatus.InvalidInput );
        }

        var quoteResponse = await _client.SpotApi.Trading.GetLiquidityPoolSwapQuoteAsync( from, to, quantity );

        // If we had a quote we can return it
        if ( quoteResponse.Success ) return Result.Ok( quoteResponse.Data );

        _logger.LogWarning( "Unable to get a quote {from}:{to}", @from, to );
        return Result.Fail<BinanceBSwapQuote>( "Asset swap is not possible", ResultStatus.InvalidInput );
    }

    public async Task<Result<BinanceBSwapResult>> Swap( string from, string to, decimal quantity )
    {
        _logger.LogDebug( "Verifying swap pool {from}:{to} exists", from, to );

        if ( ( await _poolsService.Exists( from, to ) ).Failure )
        {
            _logger.LogWarning( "Tried to swap assets with no existing pool {from}:{to}", from, to );
            return Result.Fail<BinanceBSwapResult>( "Asset swap is not possible", ResultStatus.InvalidInput );
        }

        if ( ( await _fundService.IsAvailable( from, quantity ) ).Failure )
        {
            _logger.LogCritical( "Not enough funds for swap {from}:{to} amount: {quantity}", from, to, quantity );
            return Result.Fail<BinanceBSwapResult>( "Asset swap is not possible", ResultStatus.InvalidInput );
        }

        _logger.LogInformation( "Swapping {from}:{to} amount: {quantity}", from, to, quantity );
        var swapResponse = await _client.SpotApi.Trading.LiquidityPoolSwapAsync( from, to, quantity );

        // If the swap went properly we can return the result
        if ( swapResponse.Success ) return Result.Ok( swapResponse.Data );

        _logger.LogWarning( "Unable to get a quote {from}:{to}", from, to );
        return Result.Fail<BinanceBSwapResult>( "Asset swap is not possible", ResultStatus.InvalidInput );
    }
}