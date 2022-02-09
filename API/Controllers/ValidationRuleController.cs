using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Queries;
using Models.Results;

namespace API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ValidationRuleController : ControllerBase
    {
        private readonly IValidationRuleRepository _validationRuleRepository;
        private readonly ILogger<ValidationRuleController> _logger;

        public ValidationRuleController( IValidationRuleRepository validationRuleRepository,
                                         ILogger<ValidationRuleController> logger )
        {
            _validationRuleRepository = validationRuleRepository;
            _logger                   = logger;
        }


        [HttpGet( "{id}", Name = "RuleGetById" )]
        public async Task<ActionResult<ValidationRuleDto>> GetAsync( Guid id )
        {
            return await _validationRuleRepository.GetAsync( id ).ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<Page<ValidationRuleDto>>> Query([FromQuery] ValidationRuleQuery query)
        {
            return await _validationRuleRepository.QueryAsync(query).ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync( [FromBody] ValidationRuleCreateDto rule )
        {
            var result = await _validationRuleRepository.AddAsync( rule );

            if ( result.Failure ) return result?.ToActionResult()?.Result ?? BadRequest();

            return CreatedAtRoute( "RuleGetById", new { id = result.Value.Id }, result.Value );
        }

        [HttpPut( "{id}" )]
        public async Task<ActionResult<ValidationRuleDto>> UpdateAsync(
            Guid id, [FromBody] ValidationRuleCreateDto rule )
        {
            return await _validationRuleRepository.UpdateAsync( id, rule ).ToActionResult();
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteAsync( Guid id )
        {
            return await _validationRuleRepository.DeleteAsync( id ).ToActionResult();
        }
    }
}