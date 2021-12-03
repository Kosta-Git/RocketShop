using System.Threading.Tasks;
using DataAccess.Results;
using Models.DTO;

namespace DataAccess.Repositories.Interfaces;

public interface IValidationRepository
{
    Task<Result<ValidationDto>> AddAsync(ValidationCreateDto validation);
}