using MediatR;
using Microsoft.Extensions.Logging;
using eShop.Ordering.Domain.AggregatesModel.OrderAggregate;
using eShop.Ordering.Domain.Events;

namespace eShop.Ordering.API.Application.Commands;

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CompleteOrderCommandHandler> _logger;

    public CompleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<CompleteOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(CompleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(command.OrderNumber);
        if (order == null)
        {
            return false;
        }

        order.SetCompletedStatus();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
} 
