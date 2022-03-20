using DataAccess.Entities;
using Models.DTO;
using Models.Queries;
using Models.Results;

namespace BLL.Services.Orders;

public interface IOrderService
{
    Task<Result<OrderDto>> Create( OrderCreateDto order );

    Task<Result> Approve( Guid userGuid, Guid orderGuid );

    Task<Result> Decline( Guid userGuid, Guid orderGuid );

    Task<Result<OrderDto>> Get( Guid orderGuid );

    Task<Result<Page<OrderDto>>> Get( OrderQuery query );
}