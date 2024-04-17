using Inventory.Application.Features.Inventories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.ResultPattern;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}", Name = "GetInventoryById")]
        public async Task<IResult> GetById(Guid id)
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
                case "Inventory.NotFound":
                    return Results.NotFound(error.Description);
            }

            return null!;
        }
    }
}