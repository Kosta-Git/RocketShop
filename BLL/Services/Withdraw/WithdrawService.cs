using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Spot;
using Microsoft.Extensions.Logging;
using Models.Results;

namespace BLL.Services.Withdraw;

public class WithdrawService : IWithdrawService
{
    private readonly IBinanceClient _client;
    private readonly ILogger<WithdrawService> _logger;

    public WithdrawService( IBinanceClient client, ILogger<WithdrawService> logger )
    {
        _client = client;
        _logger = logger;
    }

    public async Task<Result<BinanceWithdrawalPlaced>> Withdraw(
        string asset,
        string address,
        decimal quantity,
        string? withdrawOrderId = null,
        string? network = null,
        string? addressTag = null,
        string? name = null,
        bool? transactionFeeFlag = null,
        WalletType? walletType = null
    )
    {
        var withdrawResponse = await _client.SpotApi.Account.WithdrawAsync(
            asset,
            address,
            quantity,
            withdrawOrderId,
            network,
            addressTag,
            name,
            transactionFeeFlag,
            walletType
        );

        if ( !withdrawResponse.Success ) return Result.Fail<BinanceWithdrawalPlaced>( "Unable to withdraw" );

        return Result.Ok( withdrawResponse.Data );
    }
}