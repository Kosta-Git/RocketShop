using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataAccess;
using DataAccess.Enum;
using DataAccess.Mapping;
using DataAccess.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.DTO;

namespace DataAccess.Repositories;

public class OrderRepository : BaseRepository<OrderRepository>, IOrderRepository
{
    public OrderRepository( DatabaseContext context, ILogger<OrderRepository> logger ) : base( context, logger ) { }

    public async Task<Result<OrderDto>> GetAsync( Guid id )
    {
        var order = await _context.Orders.FirstOrDefaultAsync( order => order.Id.Equals( id ) );

        return order == null
            ? Result.Fail<OrderDto>( "Order not found", ResultStatus.NotFound )
            : Result.Ok( order.AsDto() );
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetAllAsync()
    {
        return Result.Ok( ( await _context.Orders.ToListAsync() ).Select( order => order.AsDto() ) );
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetByStatusAsync( Status status )
    {
        return await GetByStatusAsync( new[] { status } );
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetByStatusAsync( Status[] statuses )
    {
        return Result.Ok(
            ( await _context.Orders.Where( o => statuses.Contains( o.Status ) ).ToListAsync() )
           .Select( o => o.AsDto() )
        );
    }
}