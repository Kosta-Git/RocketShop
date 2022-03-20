using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;
using Models.DTO;
using Models.Enums;
using Models.Queries;
using Models.Results;

namespace DataAccess.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Result<Order>> AddAsync(Order order);
    Task<Result<Order>> GetAsync( Guid id );
    Task<Result<Page<Order>>> QueryAsync(OrderQuery query);
}