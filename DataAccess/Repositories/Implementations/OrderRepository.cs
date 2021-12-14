using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataAccess;
using DataAccess.Entities;
using DataAccess.Mapping;
using DataAccess.Repositories.Interfaces;
using DataAccess.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Enums;
using Models.Queries;

namespace DataAccess.Repositories.Implementations;

public class OrderRepository : BaseRepository<OrderRepository>, IOrderRepository
{
    public OrderRepository( DatabaseContext context, ILogger<OrderRepository> logger ) : base( context, logger ) { }

    public async Task<Result<OrderDto>> AddAsync( OrderCreateDto order )
    {
        var coin = await _context.Coins.FirstOrDefaultAsync( c => c.Id.Equals( order.CoinId ) );
        if ( coin == null ) return Result.Fail<OrderDto>( "The coin does not exist.", ResultStatus.NotFound );

        var rule = await GetValidationRuleForAmountAsync( order.Amount );
        if ( rule.Failure ) return Result.Fail<OrderDto>( rule.Error, rule.Status );

        var entity = order.AsEntity();
        entity.Coin           = coin;
        entity.ValidationRule = rule.Value;
        entity.Status         = Status.Pending;

        await _context.Orders.AddAsync( entity );
        await _context.SaveChangesAsync();

        return Result.Ok( entity.AsDto() );
    }

    public async Task<Result<OrderDto>> GetAsync( Guid id )
    {
        var order = await _context.Orders
                                  .Include( o => o.Coin )
                                  .Include( o => o.ValidationRule )
                                  .Include( o => o.Validations )
                                  .FirstOrDefaultAsync( order => order.Id.Equals( id ) );

        return order == null
            ? Result.Fail<OrderDto>( "Order not found", ResultStatus.NotFound )
            : Result.Ok( order.AsDto() );
    }

    public async Task<Result<Page<OrderDto>>> QueryAsync( OrderQuery query )
    {
        var skip = query.PageNumber * query.PageSize;
        var total = await _context.Orders
                                  .Where( order => query.Status.Contains( order.Status ) )
                                  .CountAsync();

        var totalPages = ( int )Math.Ceiling( ( decimal )total / ( decimal )query.PageSize );

        var values = await _context.Orders
                                   .Where( order => query.Status.Contains( order.Status ) )
                                   .Skip( skip )
                                   .Take( query.PageSize )
                                   .Include( o => o.Coin )
                                   .Include( o => o.ValidationRule )
                                   .Include( o => o.Validations )
                                   .ToListAsync();


        return Result.Ok( new Page<OrderDto>
        {
            Data        = values.Select( v => v.AsDto() ),
            PageNumber  = query.PageNumber,
            PageSize    = query.PageSize,
            TotalValues = total,
            TotalPages  = totalPages,
            NextPage    = totalPages <= query.PageNumber ? null : query.PageNumber + 1
        } );
    }

    private async Task<Result<ValidationRule>> GetValidationRuleForAmountAsync( float amount )
    {
        var rule = await _context.ValidationRules.FirstOrDefaultAsync( r => r.Start <= amount && amount < r.End );

        if ( rule != null ) return Result.Ok( rule );

        rule = await _context.ValidationRules.OrderBy( r => r.End ).FirstOrDefaultAsync();

        if ( rule != null ) return Result.Ok( rule );

        return Result.Fail<ValidationRule>( "No validation rule was found", ResultStatus.NotFound );
    }
}