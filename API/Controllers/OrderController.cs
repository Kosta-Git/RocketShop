using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using BLL.Services.Orders;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Enums;
using Models.Queries;
using Models.Results;

namespace API.Controllers;

[Route( "api/[controller]" )]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController( IOrderService orderService, ILogger<OrderController> logger )
    {
        _orderService = orderService;
        _logger       = logger;
    }

    [HttpGet( "{id}" )]
    public async Task<ActionResult<OrderDto>> GetAsync( Guid id )
    {
        return await _orderService.Get( id ).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<Page<OrderDto>>> QueryAsync( [FromQuery] OrderQuery query )
    {
        return await _orderService.Get( query ).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Post( [FromBody] OrderCreateDto order )
    {
        return await _orderService.Create( order ).ToActionResult();
    }

    [HttpPut( "{id}" )]
    public void Put( int id, [FromBody] string value ) { }

    [HttpDelete( "{id}" )]
    public void Delete( int id ) { }
}