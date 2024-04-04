using FluentValidation;
using Order.Application.Dto.Output;
using Order.Domain.Order;
using SharedKernel.Messaging;
using SharedKernel.ResultPattern;

namespace Order.Application.Features.Orders
{
    public class GetOrderStatus
    {
        public record Query : IQuery<OrderStatusDto>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
            }
        }

        public class Handler(IOrderRepository orderRepository) : IQueryHandler<Query, OrderStatusDto>
        {
            private readonly IOrderRepository _orderRepository = orderRepository;

            public async Task<Result<OrderStatusDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.Id);

                if (order is null)
                {
                    return Result.Failure<OrderStatusDto>(OrderErrors.NotFound(request.Id));
                }

                var orderStatus = new OrderStatusDto
                {
                    OrderId = request.Id,
                    Status = order.Status,
                    CreatedAt = order.CreatedAt
                };

                return Result.Success(orderStatus);
            }
        }
    }
}