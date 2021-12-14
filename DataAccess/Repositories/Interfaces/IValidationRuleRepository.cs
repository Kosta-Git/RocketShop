using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Results;
using Models.DTO;
using Models.Queries;

namespace DataAccess.Repositories.Interfaces;

public interface IValidationRuleRepository
{
    Task<Result<ValidationRuleDto>> AddAsync(ValidationRuleCreateDto rule);
    Task<Result<ValidationRuleDto>> GetAsync(Guid ruleId);
    Task<Result<Page<ValidationRuleDto>>> QueryAsync(ValidationRuleQuery query);
    Task<Result<ValidationRuleDto>> UpdateAsync(Guid ruleId, ValidationRuleCreateDto updatedRule);
    Task<Result> DeleteAsync(Guid ruleId);
}