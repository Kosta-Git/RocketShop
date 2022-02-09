using System;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
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

        [HttpGet("networks/{asset}")]
        public async Task<IActionResult> GetNetworks(string asset)
        {
            var userCoins = await _client.General.GetUserCoinsAsync();
           
            return Ok(
                userCoins.Data
            );
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var userCoins = await _client.General.GetUserCoinsAsync();
            //var quote = await _client.BSwap.GetQuoteAsync("BNB", "ETH", 10);

            //var pools = await _client.BSwap.GetBSwapPoolsAsync();
            //return Ok(
            //    new
            //    {
            //        userCoins.Data,
            //        pools = pools.Data.Where(p => p.Assets.Contains("USDT")),
            //    }
            //);

            //var payment = _client.WithdrawDeposit.WithdrawAsync("MATIC", "0xB58d53c21D676f1A0c4ECdE0D806cEa5717fA68d")
            return Ok();
        }
    }
}