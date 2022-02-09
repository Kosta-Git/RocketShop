using Models.Results;

namespace BLL.Services.Swaps;

public interface ISwapService
{
    Task<Result<IEnumerable<string>>> GetAll();
}