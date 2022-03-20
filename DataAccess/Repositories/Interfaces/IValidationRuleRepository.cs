using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;
using Models.DTO;
using Models.Queries;
using Models.Results;

namespace DataAccess.Repositories.Interfaces;

public interface IValidationRuleRepository
{
    Task<Result<ValidationRule>> AddAsync(ValidationRule rule);
    Task<Result<ValidationRule>> GetAsync(Guid ruleId);
    Task<Result<ValidationRule>> GetForAmountAsync(float amount);
    Task<Result<Page<ValidationRule>>> QueryAsync(ValidationRuleQuery query);
    Task<Result<ValidationRule>> UpdateAsync(Guid ruleId, ValidationRule updatedRule);
    Task<Result> DeleteAsync(Guid ruleId);
}