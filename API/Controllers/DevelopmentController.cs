using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class DevelopmentController : ControllerBase
    {
        private readonly ILogger<DevelopmentController> _logger;
        private readonly IBinanceClient _client;

        public DevelopmentController( ILogger<DevelopmentController> logger, IBinanceClient client )
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var callResult = await _client.Spot.Market.GetPriceAsync( "BTCUSDT" );

            if ( callResult.Success is false )
            {
                return BadRequest( callResult.Error?.Message ?? "Unknown error" );
            }

            return Ok( callResult.Data );
        }
    }
}