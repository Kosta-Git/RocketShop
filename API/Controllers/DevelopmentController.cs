using System;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
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

        [HttpGet( "networks/{asset}" )]
        public async Task<IActionResult> GetNetworks( string asset )
        {
            var userCoins = await _client.SpotApi.Account.GetUserAssetsAsync();

            return Ok(
                userCoins.Data
            );
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var quote     = await _client.SpotApi.Trading.GetLiquidityPoolSwapQuoteAsync( "USDT", "ADA", 5 );
            return Ok(
                new
                {
                    quote
                }
            );
        }
    }
}