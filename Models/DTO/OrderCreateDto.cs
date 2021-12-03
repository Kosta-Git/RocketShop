using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class OrderCreateDto
{
    public OrderCreateDto( Guid userGuid, string walletAddress, string network, float amount, Guid coinId )
    {
        UserGuid      = userGuid;
        WalletAddress = walletAddress;
        Network       = network;
        Amount        = amount;
        CoinId        = coinId;
    }

    [Required]
    public Guid UserGuid { get; set; }

    [Required]
    [MaxLength(512)]
    public string WalletAddress { get; set; }

    [Required]
    [MaxLength(64)]
    public string Network { get; set; }

    [Required]
    public float Amount { get; set; }

    [Required]
    public Guid CoinId { get; set; }
}