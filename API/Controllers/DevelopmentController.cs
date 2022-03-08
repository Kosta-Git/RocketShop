using System;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Spot.BSwap;
using BLL.Services.Swaps;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Results;

namespace API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class DevelopmentController : ControllerBase
    {
        private readonly IBinanceClient _client;
        private readonly ISwapService _swapService;
        private readonly ILogger<DevelopmentController> _logger;

        public DevelopmentController( IBinanceClient client, ILogger<DevelopmentController> logger, ISwapService swapService )
        {
            _client           = client;
            _logger           = logger;
            _swapService = swapService;
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
        public async Task<ActionResult<BinanceBSwapQuote>> Get([FromQuery] string asset, [FromQuery] decimal quantity)
        {
            return await _swapService.GetQuote("USDT", asset, quantity).ToActionResult();
        }
    }
}