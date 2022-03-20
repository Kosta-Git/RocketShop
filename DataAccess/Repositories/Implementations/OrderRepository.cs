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
using Models.Enums;
using Models.Queries;
using Models.Results;

namespace DataAccess.Repositories.Implementations;

public class OrderRepository : BaseRepository<OrderRepository>, IOrderRepository
{
    public OrderRepository( DatabaseContext context, ILogger<OrderRepository> logger ) : base( context, logger ) { }

    public async Task<Result<Order>> AddAsync( Order order )
    {
        order.Status = Status.Pending;
        await _context.Orders.AddAsync( order );
        await _context.SaveChangesAsync();

        return Result.Ok( order );
    }

    public async Task<Result<Order>> GetAsync( Guid id )
    {
        var order = await _context.Orders
                                  .Include( o => o.ValidationRule )
                                  .Include( o => o.Validations )
                                  .FirstOrDefaultAsync( order => order.Id.Equals( id ) );

        return order == null
            ? Result.Fail<Order>( "Order not found", ResultStatus.NotFound )
            : Result.Ok( order );
    }

    public async Task<Result<Page<Order>>> QueryAsync( OrderQuery query )
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
                                   .Include( o => o.ValidationRule )
                                   .Include( o => o.Validations )
                                   .ToListAsync();


        return Result.Ok( new Page<Order>
        {
            Data        = values,
            PageNumber  = query.PageNumber,
            PageSize    = query.PageSize,
            TotalValues = total,
            TotalPages  = totalPages,
            NextPage    = totalPages <= query.PageNumber ? null : query.PageNumber + 1
        } );
    }
}