using Inventory.Application.Features.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.ResultPattern;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetById.Query
            {
                Id = id
            });

            return result.IsSuccess ? Results.Ok(result.Value) : HandleError(result.Error);
        }

        private static IResult HandleError(Error error)
        {
            switch (error.Code)
            {
                case "Product.NotFound":
                    return Results.NotFound(error.Description);
            }

            return null!;
        }
    }
}