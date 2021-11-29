using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Repositories;
using DataAccess.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DTO;

namespace API.Controllers;

[Route( "api/[controller]" )]
[ApiController]
public class CoinController : ControllerBase
{
    private readonly ICoinRepository _coinRepository;
    private readonly ILogger<CoinController> _logger;

    public CoinController( ICoinRepository coinRepository, ILogger<CoinController> logger )
    {
        _coinRepository = coinRepository;
        _logger         = logger;
    }

    [HttpGet( "{id}", Name = "GetById" )]
    public async Task<ActionResult<CoinDto>> GetAsync( Guid id )
    {
        return await _coinRepository.GetAsync( id ).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoinDto>>> GetAllAsync()
    {
        return await _coinRepository.GetAllAsync().ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync( [FromBody] CoinCreateDto coin )
    {
        var result = await _coinRepository.AddAsync( coin );

        if ( result.Failure ) return result?.ToActionResult()?.Result ?? BadRequest();

        return CreatedAtRoute( "GetById", new { id = result.Value.Id }, result.Value );
    }
}