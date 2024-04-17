using FluentValidation;
using Inventory.Application.Dtos.Output;
using Inventory.Domain.Inventory;
using SharedKernel.Messaging;
using SharedKernel.ResultPattern;

namespace Inventory.Application.Features.Inventories
{
    public class GetById
    {
        public record Query : IQuery<InventoryResponse>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler(IInventoryRepository inventoryRepository) : IQueryHandler<Query, InventoryResponse>
        {
            private readonly IInventoryRepository _inventoryRepository = inventoryRepository;

            public async Task<Result<InventoryResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var inventory = await _inventoryRepository.GetByIdAsync(request.Id);

                if (inventory is null)
                {
                    return Result.Failure<InventoryResponse>(InventoryErrors.NotFound(request.Id));
                }

                return Result.Success(new InventoryResponse
                {
                    ProductId = inventory.ProductId,
                    Quantity = inventory.Quantity,
                });
            }
        }
    }
}