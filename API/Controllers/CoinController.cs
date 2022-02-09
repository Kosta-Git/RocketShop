using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Objects.Spot.WalletData;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Results;

namespace API.Controllers;

[Route( "api/[controller]" )]
[ApiController]
public class CoinController : ControllerBase
{
    private readonly ICoinService _coinService;
    private readonly ILogger<CoinController> _logger;

    public CoinController( ILogger<CoinController> logger, ICoinService coinService )
    {
        _logger      = logger;
        _coinService = coinService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BinanceUserCoin>>> GetAllAsync()
    {
        return await _coinService.GetCoins().ToActionResult();
    }

    [HttpGet( "{coin}" )]
    public async Task<ActionResult<BinanceUserCoin>> GetAsync( string coin )
    {
        return await _coinService.GetCoin( coin ).ToActionResult();
    }

    [HttpGet( "Networks/{coin}" )]
    public async Task<ActionResult<IEnumerable<BinanceNetwork>>> GetNetworksAsync( string coin )
    {
        return await _coinService.GetCoinNetworks( coin ).ToActionResult();
    }
}