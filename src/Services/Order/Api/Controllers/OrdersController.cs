using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetById.Query
            {
                Id = id
            });

            return Results.Ok(result.Value);
        }

        [HttpPost("")]
        public async Task<IResult> CreateOrder([FromBody] CreateOrder.Command command)
        {
            var result = await _mediator.Send(command);

            return Results.CreatedAtRoute("GetById", new { id = result.Value });
        }
    }
}