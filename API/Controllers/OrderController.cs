﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Enum;
using DataAccess.Repositories;
using DataAccess.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DTO;

namespace API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger )
        {
            _orderRepository = orderRepository;
            _logger          = logger;
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<OrderDto>> GetAsync( Guid id )
        {
            return await _orderRepository.GetAsync( id ).ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync().ToActionResult();
        }

        [HttpPost( "/Query" )]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByStatusAsync( [FromBody] Status[] statuses )
        {
            if ( !statuses.Any() ) return BadRequest();

            return await _orderRepository.GetByStatusAsync( statuses ).ToActionResult();
        }


        [HttpPost]
        public void Post( [FromBody] string value ) { }

        [HttpPut( "{id}" )]
        public void Put( int id, [FromBody] string value ) { }

        [HttpDelete( "{id}" )]
        public void Delete( int id ) { }
    }
}