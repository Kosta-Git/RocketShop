using DataAccess.Entities;
using DataAccess.Mapping;
using DataAccess.Repositories.Interfaces;
using DataAccess.Services;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Queries;
using Models.Results;

namespace BLL.Services.Orders;

public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly ICoinService _coinService;
    private readonly IOrderRepository _orderRepository;
    private readonly IValidationRepository _validationRepository;
    private readonly IValidationRuleRepository _validationRuleRepository;

    public OrderService( ILogger<OrderService> logger, IOrderRepository orderRepository,
                         IValidationRepository validationRepository, ICoinService coinService,
                         IValidationRuleRepository validationRuleRepository )
    {
        _logger                   = logger;
        _orderRepository          = orderRepository;
        _validationRepository     = validationRepository;
        _coinService              = coinService;
        _validationRuleRepository = validationRuleRepository;
    }

    public async Task<Result<OrderDto>> Create( OrderCreateDto orderDto )
    {
        // Verify that the coin exists
        var coinResult = await _coinService.GetCoin( orderDto.Coin.ToUpper() );
        if ( coinResult.Failure )
            return Result.Fail<OrderDto>( "Please enter a valid coin", ResultStatus.InvalidInput );

        // Verify that a rule exists for amount
        var ruleResult = await _validationRuleRepository.GetForAmountAsync( orderDto.Amount );
        if ( ruleResult.Failure )
            return Result.Fail<OrderDto>( "Unable to find rule for amount", ResultStatus.InternalError );

        // Add rule to order entity
        var order = orderDto.AsEntity();
        order.Coin           = order.Coin.ToUpper();
        order.ValidationRule = ruleResult.Value;

        var orderResult = await _orderRepository.AddAsync( order );

        return orderResult.Failure
            ? Result.Fail<OrderDto>( orderResult.Error, orderResult.Status )
            : Result.Ok( orderResult.Value.AsDto() );
    }

    public async Task<Result> Approve( Guid userGuid, Guid orderGuid )
    {
        return await ValidateOrder( userGuid, orderGuid, true );
    }

    public async Task<Result> Decline( Guid userGuid, Guid orderGuid )
    {
        return await ValidateOrder( userGuid, orderGuid, false );
    }

    public async Task<Result<OrderDto>> Get( Guid orderGuid )
    {
        var result = await _orderRepository.GetAsync( orderGuid );

        return result.Failure
            ? Result.Fail<OrderDto>( result.Error, result.Status )
            : Result.Ok( result.Value.AsDto() );
    }

    public async Task<Result<Page<OrderDto>>> Get( OrderQuery query )
    {
        var result = await _orderRepository.QueryAsync( query );

        if ( result.Failure ) Result.Fail<Page<OrderDto>>( result.Error, result.Status );

        var page = result.Value;

        return Result.Ok( Page.Convert(page, OrderExtensions.AsDto) );
    }

    private async Task<Result> ValidateOrder( Guid userGuid, Guid orderGuid, bool validate )
    {
        return await _validationRepository.AddAsync( new ValidationCreateDto( userGuid, orderGuid, validate ) );
    }
}