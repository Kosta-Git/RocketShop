using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class OrderCreateDto
{
    public OrderCreateDto(
        Guid userGuid,
        string walletAddress,
        string walletAddressTag,
        string network,
        float amount,
        string coin
    )
    {
        UserGuid         = userGuid;
        WalletAddress    = walletAddress;
        WalletAddressTag = walletAddressTag;
        Network          = network;
        Amount           = amount;
        Coin             = coin;
    }

    [Required] public Guid UserGuid { get; set; }

    [Required] [MaxLength( 512 )] public string WalletAddress { get; set; }

    [MaxLength( 512 )] public string WalletAddressTag { get; set; }

    [Required] [MaxLength( 64 )] public string Network { get; set; }

    [Required] public float Amount { get; set; }

    [Required] public string Coin { get; set; }
}