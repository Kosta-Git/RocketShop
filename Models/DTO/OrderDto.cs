namespace Models.DTO;

public class OrderDto
{
    public OrderDto(
        Guid id,
        Guid userGuid,
        string walletAddress,
        string walletAddressTag,
        string network,
        float amount,
        string coin,
        string status,
        ValidationRuleDto validationRule,
        List<ValidationDto> validations )
    {
        Id               = id;
        UserGuid         = userGuid;
        WalletAddress    = walletAddress;
        WalletAddressTag = walletAddressTag;
        Network          = network;
        Amount           = amount;
        Coin             = coin;
        Status           = status;
        ValidationRule   = validationRule;
        Validations      = validations;
    }

    public Guid Id { get; set; }

    public Guid UserGuid { get; set; }

    public string WalletAddress { get; set; }

    public string WalletAddressTag { get; set; }

    public string Network { get; set; }

    public float Amount { get; set; }

    public string Coin { get; set; }

    public string Status { get; set; }

    public ValidationRuleDto ValidationRule { get; set; }

    public List<ValidationDto> Validations { get; set; }
}