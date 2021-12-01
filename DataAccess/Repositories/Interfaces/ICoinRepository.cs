using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Results;
using Models.DTO;

namespace DataAccess.Repositories.Interfaces;

public interface ICoinRepository
{
    Task<Result<IEnumerable<CoinDto>>> GetAllAsync();
    Task<Result<CoinDto>> GetAsync( Guid id );
    Task<Result<CoinDto>> AddAsync( CoinCreateDto coin );
}