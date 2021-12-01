namespace Models.DTO;

public class OrderDto
{
    //public ValidationRule ValidationRule { get; set; }

    public OrderDto( Guid userGuid, string walletAddress, string network, float amount, CoinDto coin, string status )
    {
        UserGuid      = userGuid;
        WalletAddress = walletAddress;
        Network       = network;
        Amount        = amount;
        Coin          = coin;
        Status        = status;
    }

    public Guid UserGuid { get; set; }

    public string WalletAddress { get; set; }

    public string Network { get; set; }

    public float Amount { get; set; }

    public CoinDto Coin { get; set; }

    public string Status { get; set; }
}