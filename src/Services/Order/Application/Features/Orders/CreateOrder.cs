using FluentValidation;
using Order.Application.Abstractions;
using Order.Application.Data;
using Order.Application.Dto.Input;
using Order.Domain.Order;
using SharedKernel.Messaging;
using SharedKernel.ResultPattern;
using SharedMessage;
using SharedMessage.Commands;

namespace Order.Application.Features.Orders
{
    public class CreateOrder
    {
        public record Command : CreateOrderDto, ICommand<Guid>
        {
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
            }
        }

        public class Handler(IOrderRepository orderRepository,
                             IUnitOfWork unitOfWork,
                             IMassTransitService massTransitService) : ICommandHandler<Command, Guid>
        {
            private readonly IOrderRepository _orderRepository = orderRepository;
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IMassTransitService _massTransitService = massTransitService;

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = Domain.Order.Order.Create(request.UserId);

                order.AddOrderDetail(request.ProductId, request.Quantity);

                await _orderRepository.CreateOrderAsync(order);

                var createOrderCommand = new CreateOrderCommand
                {
                    OrderId = order.Id,
                    UserId = request.UserId,
                    OrderDetails = order.OrderDetails.Select(o => new SharedMessage.Entity.OrderDetail
                    {
                        ProductId = request.ProductId,
                        Quantity = request.Quantity
                    }).ToList()
                };

                await _massTransitService.SendAsync(createOrderCommand, Constants.CreateOrderCommandQueue);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return order.Id;
            }
        }
    }
}