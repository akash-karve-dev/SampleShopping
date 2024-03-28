using FluentValidation;
using Order.Application.Data;
using Order.Application.Dto.Input;
using Order.Domain.Order;
using SharedKernel.Messaging;
using SharedKernel.ResultPattern;

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

        public class Handler(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : ICommandHandler<Command, Guid>
        {
            private readonly IOrderRepository _orderRepository = orderRepository;
            private readonly IUnitOfWork _unitOfWork = unitOfWork;

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = Order.Domain.Order.Order.Create(request.UserId);

                order.AddOrderDetail(request.ProductId, request.Quantity);

                await _orderRepository.CreateOrderAsync(order);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success(order.Id);
            }
        }
    }
}