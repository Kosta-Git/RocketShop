using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class ValidationRuleCreateDto
{
    public ValidationRuleCreateDto( float start, float end, uint confirmations, bool enabled )
    {
        Start         = start;
        End           = end;
        Confirmations = confirmations;
        Enabled       = enabled;
    }

    [Required] public float Start { get; set; }

    [Required] public float End { get; set; }

    [Required] public uint Confirmations { get; set; }

    [Required] public bool Enabled { get; set; }
}