using MediatR;
using Order.Domain.Order;
using SharedKernel.Messaging;
using SharedKernel.ResultPattern;

namespace Order.Application.Features.Orders
{
    public class GetById
    {
        public record Query : IQuery<Domain.Order.Order>
        {
            public Guid Id { get; set; }
        }

        public class Handler(IOrderRepository orderRepository) : IQueryHandler<Query, Domain.Order.Order>
        {
            private readonly IOrderRepository _orderRepository = orderRepository;

            async Task<Result<Domain.Order.Order>> IRequestHandler<Query, Result<Domain.Order.Order>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var domainOrder = await _orderRepository.GetByIdAsync(request.Id);

                if (domainOrder is null)
                {
                    return Result.Failure<Domain.Order.Order>(OrderErrors.NotFound(request.Id));
                }

                return Result.Success(domainOrder);
            }
        }
    }
}