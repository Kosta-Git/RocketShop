namespace Models.DTO;

public class OrderDto
{
    public Guid UserGuid { get; set; }

    public string WalletAddress { get; set; }

    public string Network { get; set; }

    public float Amount { get; set; }

    public CoinDto Coin { get; set; }

    public string Status { get; set; }

    //public ValidationRule ValidationRule { get; set; }
}