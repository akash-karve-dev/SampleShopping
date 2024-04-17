using FluentValidation;
using Inventory.Application.Dtos.Output;
using Inventory.Domain.Product;
using SharedKernel.Messaging;
using SharedKernel.ResultPattern;

namespace Inventory.Application.Features.Products
{
    public class GetById
    {
        public record Query : IQuery<ProductResponse>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
            }
        }

        public class Handler(IProductRepository productRepository) : IQueryHandler<Query, ProductResponse>
        {
            private readonly IProductRepository _productRepository = productRepository;

            public async Task<Result<ProductResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(request.Id);

                if (product is null)
                {
                    return Result.Failure<ProductResponse>(ProductErrors.NotFound(request.Id));
                }

                return Result.Success(new ProductResponse
                {
                    Name = product.Name,
                    ProductCategory = product.ProductCategory
                });
            }
        }
    }
}