using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class DevelopmentController : ControllerBase
    {
        private readonly IBinanceClient _client;
        private readonly ILogger<DevelopmentController> _logger;

        public DevelopmentController( IBinanceClient client, ILogger<DevelopmentController> logger )
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var accountInfo = await _client.General.GetAccountInfoAsync();
            var userCoins   = await _client.General.GetUserCoinsAsync();
            var quote       = await _client.BSwap.GetQuoteAsync( "BNB", "ETH", 10 );
            var pools       = await _client.BSwap.GetPoolLiquidityInfoAsync();

            var data = new
            {
                accountInfo = accountInfo.Data,
                userCoins   = userCoins.Data,
                quote       = quote.Data,
                pools       = pools.Data,
            };

            return Ok( data );
        }
    }
}