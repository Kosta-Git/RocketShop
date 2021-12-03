using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Enum;
using DataAccess.Results;
using Models.DTO;

namespace DataAccess.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Result<OrderDto>> AddAsync(OrderCreateDto order);
    Task<Result<OrderDto>> GetAsync( Guid id );
    Task<Result<IEnumerable<OrderDto>>> GetAllAsync();
    Task<Result<IEnumerable<OrderDto>>> GetByStatusAsync( Status status );
    Task<Result<IEnumerable<OrderDto>>> GetByStatusAsync( Status[] statuses );
}