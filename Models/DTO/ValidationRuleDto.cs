namespace Models.DTO;

public class ValidationRuleDto
{
    public ValidationRuleDto( Guid id, float start, float end, uint confirmations, bool enabled )
    {
        Id            = id;
        Start         = start;
        End           = end;
        Confirmations = confirmations;
        Enabled       = enabled;
    }

    public Guid Id { get; set; }

    public float Start { get; set; }

    public float End { get; set; }

    public uint Confirmations { get; set; }

    public bool Enabled { get; set; }
}