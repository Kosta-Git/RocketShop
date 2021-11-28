using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Enum;
using DataAccess.Results;
using Models.DTO;

namespace DataAccess.Repositories;

public interface IOrderRepository
{
    Task<Result<OrderDto>> GetAsync( Guid id );
    Task<Result<IEnumerable<OrderDto>>> GetAllAsync();
    Task<Result<IEnumerable<OrderDto>>> GetByStatusAsync( Status status );
    Task<Result<IEnumerable<OrderDto>>> GetByStatusAsync( Status[] statuses );
}