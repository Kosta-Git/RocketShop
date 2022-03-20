using DataAccess.Entities;
using Models.DTO;

namespace DataAccess.Mapping;

public static class ValidationRuleExtensions
{
    public static ValidationRuleDto AsDto( this ValidationRule rule )
    {
        return new ValidationRuleDto( rule.Id, rule.Start, rule.End, rule.Confirmations, rule.Enabled );
    }

    public static ValidationRule AsEntity( this ValidationRuleCreateDto rule )
    {
        return new ValidationRule
        {
            Start         = rule.Start,
            End           = rule.End,
            Confirmations = rule.Confirmations,
            Enabled       = rule.Enabled
        };
    }

    public static ValidationRule AsEntity( this ValidationRuleDto rule )
    {
        return new ValidationRule
        {
            Id            = rule.Id,
            Start         = rule.Start,
            End           = rule.End,
            Confirmations = rule.Confirmations,
            Enabled       = rule.Enabled
        };
    }
}