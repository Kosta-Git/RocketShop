using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataAccess;
using DataAccess.Mapping;
using DataAccess.Repositories.Interfaces;
using DataAccess.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Enums;

namespace DataAccess.Repositories.Implementations;

public class ValidationRepository : BaseRepository<ValidationRepository>, IValidationRepository
{
    public ValidationRepository( DatabaseContext context, ILogger<ValidationRepository> logger ) : base(
        context, logger ) { }

    public async Task<Result<ValidationDto>> AddAsync( ValidationCreateDto validation )
    {
        if ( await _context.Validations.AnyAsync( v => v.UserGuid == validation.UserGuid &&
                                                       v.Order.Id == validation.OrderId ) )
        {
            return Result.Fail<ValidationDto>( "You have already voted on this order.", ResultStatus.InvalidInput );
        }

        var order = await _context.Orders
                                  .Include( o => o.Validations )
                                  .Include( o => o.ValidationRule )
                                  .FirstOrDefaultAsync( o => o.Id == validation.OrderId );

        if ( order == null )
        {
            return Result.Fail<ValidationDto>( "Order was not found!", ResultStatus.NotFound );
        }

        order.Validations.Add(validation.AsEntity());

        var accepted = order.Validations.Count( v => v.Accepted );
        var required = order.ValidationRule.Confirmations;

        if ( accepted >= required )
        {
            order.Status = Status.Approved;
            // TODO: Trigger transaction
        }

        await _context.SaveChangesAsync();

        return Result.Ok( order.Validations[-1].AsDto() );
    }
}