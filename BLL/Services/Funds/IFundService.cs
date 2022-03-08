using Models.Results;

namespace BLL.Services.Funds;

public interface IFundService
{
    Task<Result<decimal>> GetAvailable( string asset );
    Task<Result> IsAvailable( string asset, decimal amount );
}