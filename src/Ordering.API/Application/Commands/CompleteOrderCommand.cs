using MediatR;

namespace eShop.Ordering.API.Application.Commands;

public class CompleteOrderCommand : IRequest<bool>
{
    public int OrderNumber { get; private set; }

    public CompleteOrderCommand(int orderNumber)
    {
        OrderNumber = orderNumber;
    }
} 
