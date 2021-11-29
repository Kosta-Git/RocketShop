using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using DataAccess.Repositories;
using DataAccess.Results;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Moq;
using Xunit;

namespace UnitTests.Controllers;

public class CoinControllerTests
{
    private readonly Mock<ILogger<CoinController>> _loggerStub = new();
    private readonly Mock<ICoinRepository> _repositoryStub = new();

    [Fact( DisplayName = "Get coin by id with non existing ID" )]
    public async Task GetByCoinIdNotFound()
    {
        _repositoryStub.Setup( r => r.GetAsync( It.IsAny<Guid>() ) )
                       .ReturnsAsync( Result.Fail<CoinDto>( "Coin not found", ResultStatus.NotFound ) );
        var controller = new CoinController( _repositoryStub.Object, _loggerStub.Object );

        var result = ( await controller.GetAsync( Guid.NewGuid() ) ).Result as ObjectResult;

        Assert.NotNull( result );
        result.StatusCode.Should().Be( 404 );
    }

    [Fact( DisplayName = "Get coin by id with existing ID" )]
    public async Task GetByCoinId()
    {
        var coin = CreateCoin();
        _repositoryStub.Setup( r => r.GetAsync( It.IsAny<Guid>() ) )
                       .ReturnsAsync( Result.Ok( coin ) );
        var controller = new CoinController( _repositoryStub.Object, _loggerStub.Object );

        var result = ( await controller.GetAsync( Guid.NewGuid() ) ).Result as ObjectResult;

        Assert.NotNull( result );
        result.StatusCode.Should().Be( 200 );
        result.Value.Should().BeOfType<CoinDto>();
        result.Value.Should().Be( coin );
    }

    [Fact( DisplayName = "Get all coins when no coins exists" )]
    public async Task GetAllCoinsNoCoinsExists()
    {
        _repositoryStub.Setup( r => r.GetAllAsync() )
                       .ReturnsAsync( Result.Ok( Enumerable.Empty<CoinDto>() ) );
        var controller = new CoinController( _repositoryStub.Object, _loggerStub.Object );

        var result = ( await controller.GetAllAsync() ).Result as ObjectResult;

        Assert.NotNull( result );
        result.StatusCode.Should().Be( 200 );
        result.Value.Should().Be( Enumerable.Empty<CoinDto>() );
    }

    [Fact]
    public async Task GetAllCoins()
    {
        var coins = new List<CoinDto>
        {
            CreateCoin(),
            CreateCoin(),
            CreateCoin()
        };

        _repositoryStub.Setup( r => r.GetAllAsync() )
                       .ReturnsAsync( Result.Ok( coins.AsEnumerable() ) );
        var controller = new CoinController( _repositoryStub.Object, _loggerStub.Object );

        var result = ( await controller.GetAllAsync() ).Result as ObjectResult;

        Assert.NotNull( result );
        result.StatusCode.Should().Be( 200 );
        result.Value.Should().Be( coins );
    }

    private static CoinDto CreateCoin()
    {
        return new CoinDto( Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() );
    }
}