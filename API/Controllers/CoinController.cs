using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Objects.Spot.WalletData;
using BLL.Services.Swaps;
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
    private readonly ISwapService _swapService;
    private readonly ILogger<CoinController> _logger;

    public CoinController( ILogger<CoinController> logger, ICoinService coinService, ISwapService swapService )
    {
        _logger      = logger;
        _coinService = coinService;
        _swapService = swapService;
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

    [HttpGet( "Purchasable" )]
    public async Task<ActionResult<IEnumerable<string>>> GetPurchasableAsync()
    {
        return await _swapService.GetAll().ToActionResult();
    }
}