using Models.Results;

namespace BLL.Services.Swaps;

public interface ISwapPoolsService
{
    Task<Result<IEnumerable<string>>> GetAll();
}