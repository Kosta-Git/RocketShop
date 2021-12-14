using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Queries;

public class ValidationRuleQuery : PageQuery
{
    [Required] public bool Enabled { get; set; } = true;

    [Required] public float StartValue { get; set; } = 0;

    [Required] public float EndValue { get; set; } = 1_000_000_000;
}