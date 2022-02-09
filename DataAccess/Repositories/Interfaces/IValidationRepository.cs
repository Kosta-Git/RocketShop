using System.Threading.Tasks;
using Models.DTO;
using Models.Results;

namespace DataAccess.Repositories.Interfaces;

public interface IValidationRepository
{
    Task<Result<ValidationDto>> AddAsync(ValidationCreateDto validation);
}