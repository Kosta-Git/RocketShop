using DataAccess.DataAccess;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories;

public abstract class BaseRepository<T>
{
    protected readonly DatabaseContext _context;
    protected readonly ILogger<T> _logger;

    protected BaseRepository( DatabaseContext context, ILogger<T> logger )
    {
        _context = context;
        _logger  = logger;
    }
}