using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataAccess;
using DataAccess.Entities;
using DataAccess.Mapping;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Queries;
using Models.Results;

namespace DataAccess.Repositories.Implementations;

public class ValidationRuleRepository : BaseRepository<ValidationRuleRepository>, IValidationRuleRepository
{
    public ValidationRuleRepository( DatabaseContext context, ILogger<ValidationRuleRepository> logger ) : base(
        context, logger ) { }

    public async Task<Result<ValidationRuleDto>> AddAsync( ValidationRuleCreateDto rule )
    {
        var createdRule = ( await _context.ValidationRules.AddAsync( rule.AsEntity() ) ).Entity;
        await _context.SaveChangesAsync();

        return Result.Ok( createdRule.AsDto() );
    }

    public async Task<Result<ValidationRuleDto>> GetAsync( Guid ruleId )
    {
        var rule = await _context.ValidationRules.FirstOrDefaultAsync( r => r.Id.Equals( ruleId ) );

        return rule == null
            ? Result.Fail<ValidationRuleDto>( "Unable to find rule", ResultStatus.NotFound )
            : Result.Ok( rule.AsDto() );
    }

    public async Task<Result<Page<ValidationRuleDto>>> QueryAsync( ValidationRuleQuery query )
    {
        var skip = query.PageNumber * query.PageSize;
        
        var total = await _context.ValidationRules
                                  .Where( rule => rule.Start   >= query.StartValue &&
                                                  rule.End     <= query.EndValue   &&
                                                  rule.Enabled == query.Enabled
                                   )
                                  .CountAsync();

        var totalPages = ( int )Math.Ceiling( ( decimal )total / ( decimal )query.PageSize );

        var values = await _context.ValidationRules
                                   .Where( rule => rule.Start   >= query.StartValue &&
                                                   rule.End     <= query.EndValue   &&
                                                   rule.Enabled == query.Enabled
                                    )
                                   .Skip( skip )
                                   .Take( query.PageSize )
                                   .ToListAsync();


        return Result.Ok( new Page<ValidationRuleDto>
        {
            Data        = values.Select( v => v.AsDto() ),
            PageNumber  = query.PageNumber,
            PageSize    = query.PageSize,
            TotalValues = total,
            TotalPages  = totalPages,
            NextPage    = totalPages <= query.PageNumber ? null : query.PageNumber + 1
        } );
    }

    public async Task<Result<ValidationRuleDto>> UpdateAsync( Guid ruleId, ValidationRuleCreateDto updatedRule )
    {
        var rule = await _context.ValidationRules.FirstOrDefaultAsync( r => r.Id.Equals( ruleId ) );

        if ( rule == null ) return Result.Fail<ValidationRuleDto>( "Unable to find rule", ResultStatus.NotFound );

        rule.Start         = updatedRule.Start;
        rule.End           = updatedRule.End;
        rule.Enabled       = updatedRule.Enabled;
        rule.Confirmations = updatedRule.Confirmations;

        await _context.SaveChangesAsync();

        return Result.Ok( rule.AsDto() );
    }

    public async Task<Result> DeleteAsync( Guid ruleId )
    {
        _context.ValidationRules.Remove( new ValidationRule { Id = ruleId } );
        await _context.SaveChangesAsync();

        return Result.Ok();
    }
}