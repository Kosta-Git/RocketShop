using System;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients;
using BLL.Services.Coins;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Models.Results;
using Moq;
using Xunit;

namespace UnitTests.Services;

public class CoinServiceTests
{
    private readonly Mock<ILogger<CoinService>> _loggerStub = new();
    private readonly Mock<IMemoryCache> _cacheStub = new();
    private readonly Mock<IBinanceClient> _clientStub = new();
    private readonly Mock<IBinanceClientGeneral> _clientGeneralStub = new();
}