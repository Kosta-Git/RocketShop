using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTO;
using Models.Enums;
using Models.Queries;
using Models.Results;

namespace DataAccess.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Result<OrderDto>> AddAsync(OrderCreateDto order);
    Task<Result<OrderDto>> GetAsync( Guid id );
    Task<Result<Page<OrderDto>>> QueryAsync(OrderQuery query);
}