using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot;
using Models.Results;

namespace BLL.Services.Withdraw;

public interface IWithdrawService
{
    public Task<Result<BinanceWithdrawalPlaced>> Withdraw(
        string asset,
        string address,
        decimal quantity,
        string? withdrawOrderId = null,
        string? network = null,
        string? addressTag = null,
        string? name = null,
        bool? transactionFeeFlag = null,
        WalletType? walletType = null
    );
}