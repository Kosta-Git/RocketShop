using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.BSwap;
using BLL.Services.SwapPools;
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
    private readonly ISwapPoolsService _swapPoolsService;
    private readonly ILogger<CoinController> _logger;

    public CoinController( ILogger<CoinController> logger, ICoinService coinService, ISwapPoolsService swapPoolsService )
    {
        _logger      = logger;
        _coinService = coinService;
        _swapPoolsService = swapPoolsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BinanceUserAsset>>> GetAllAsync()
    {
        return await _coinService.GetCoins().ToActionResult();
    }

    [HttpGet( "{coin}" )]
    public async Task<ActionResult<BinanceUserAsset>> GetAsync( string coin )
    {
        return await _coinService.GetCoin( coin ).ToActionResult();
    }

    [HttpGet( "Networks/{coin}" )]
    public async Task<ActionResult<IEnumerable<BinanceNetwork>>> GetNetworksAsync( string coin )
    {
        return await _coinService.GetCoinNetworks( coin ).ToActionResult();
    }

    [HttpGet( "Purchasable" )]
    public async Task<ActionResult<IEnumerable<BinanceBSwapPool>>> GetPurchasableAsync()
    {
        return await _swapPoolsService.GetAll().ToActionResult();
    }
}