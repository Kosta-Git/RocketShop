using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataAccess;
using DataAccess.Exceptions;
using DataAccess.Mapping;
using DataAccess.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Npgsql;

namespace DataAccess.Repositories;

public class CoinRepository : BaseRepository<CoinRepository>, ICoinRepository
{
    public CoinRepository( DatabaseContext context, ILogger<CoinRepository> logger ) : base( context, logger ) { }

    public async Task<Result<CoinDto>> GetAsync( Guid id )
    {
        var coin = await _context.Coins.FirstOrDefaultAsync( c => c.Id.Equals( id ) );

        return coin == null
            ? Result.Fail<CoinDto>( "Coin not found", ResultStatus.NotFound )
            : Result.Ok( coin.AsDto() );
    }

    public async Task<Result<IEnumerable<CoinDto>>> GetAllAsync()
    {
        return Result.Ok( ( await _context.Coins.ToListAsync() ).Select( c => c.AsDto() ) );
    }

    public async Task<Result<CoinDto>> AddAsync( CoinCreateDto coin )
    {
        var createdCoin = ( await _context.Coins.AddAsync( coin.AsCoin() ) ).Entity;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch ( DbUpdateException e )
        {
            var innerException = e.InnerException as PostgresException;
            switch ( SqlState.Parse( innerException?.SqlState ?? string.Empty ) )
            {
            case SqlException.Duplicate:
                _logger.LogError(
                    "Trying to create already existing coin: {CoinName}, {CoinIdentifier}, SqlState: {SqlState}",
                    coin.Name,
                    coin.Identifier,
                    innerException?.SqlState ?? string.Empty
                );

                return Result.Fail<CoinDto>( $"Coin {coin.Name} [{coin.Identifier}] already exists.",
                                             ResultStatus.AlreadyExists );
            default:
                _logger.LogError(
                    "Unidentified Sql error creating coin: {CoinName}, {CoinIdentifier}, SqlState: {SqlState}",
                    coin.Name,
                    coin.Identifier,
                    innerException?.SqlState ?? string.Empty
                );

                return Result.Fail<CoinDto>( "Unknown error" );
            }
        }
        catch ( Exception )
        {
            return Result.Fail<CoinDto>( "Unknown error" );
        }

        return Result.Ok( createdCoin.AsDto() );
    }
}