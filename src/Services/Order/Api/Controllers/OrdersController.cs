using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders;
using SharedKernel.ResultPattern;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IResult> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetById.Query
            {
                Id = id
            });

            return result.IsSuccess ? Results.Ok(result.Value) : HandlerError(result);
        }

        [HttpGet("{id}/status", Name = "GetOrderStatus")]
        public async Task<IResult> GetOrderStatus([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetOrderStatus.Query
            {
                Id = id
            });

            return result.IsSuccess ? Results.Ok(result.Value) : HandlerError(result);
        }

        [HttpPost("")]
        public async Task<IResult> CreateOrder([FromBody] CreateOrder.Command command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Results.Accepted("GetOrderStatus", new { id = result.Value }) : HandlerError(result);
        }

        private IResult HandlerError(Result result)
        {
            if (result.IsSuccess)
            {
                throw new ArgumentException("Cannot send failure reponse for successful request");
            }

            switch (result.Error.Code)
            {
                case "Order.NotFound":
                    return Results.NotFound(new ProblemDetails
                    {
                        Type = result.Error.Code,
                        Detail = result.Error.Description
                    });
            }

            throw new Exception($"Unhandled ErrorCode: [{result.Error.Code}]");
        }
    }
}