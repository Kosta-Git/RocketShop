namespace Models.DTO;

public class OrderDto
{
    public OrderDto(
        Guid id,
        Guid userGuid,
        string walletAddress,
        string network,
        float amount,
        CoinDto coin,
        string status,
        ValidationRuleDto validationRule,
        List<ValidationDto> validations )
    {
        Id             = id;
        UserGuid       = userGuid;
        WalletAddress  = walletAddress;
        Network        = network;
        Amount         = amount;
        Coin           = coin;
        Status         = status;
        ValidationRule = validationRule;
        Validations    = validations;
    }

    public Guid Id { get; set; }

    public Guid UserGuid { get; set; }

    public string WalletAddress { get; set; }

    public string Network { get; set; }

    public float Amount { get; set; }

    public CoinDto Coin { get; set; }

    public string Status { get; set; }

    public ValidationRuleDto ValidationRule { get; set; }

    public List<ValidationDto> Validations { get; set; }
}