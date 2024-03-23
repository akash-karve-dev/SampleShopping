using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.ResultPattern;
using User.Application.Dto.Input;
using User.Application.Features.User;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var command = new CreateUser.Command
            {
                Email = createUserDto.Email,
                Name = createUserDto.Name
            };

            var result = await _mediator.Send(command);

            return result.IsSuccess ? Results.CreatedAtRoute("GetUserById", new { id = result.Value }) : HandlerError(result);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IResult> GetUser([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetUserById.Query
            {
                Id = id
            });
            return result.IsSuccess ? Results.Ok(result.Value) : HandlerError(result);
        }

        /*
         * TODO
         * Handle this is elegant way
         */

        private static IResult HandlerError(Result result)
        {
            if (result.IsSuccess)
            {
                throw new ArgumentException("Cannot send failure reponse for successful request");
            }

            switch (result.Error.Code)
            {
                case "User.NotFound":
                    return Results.NotFound(new ProblemDetails
                    {
                        Type = result.Error.Code,
                        Detail = result.Error.Description
                    });

                case "User.AlreadyExist":
                    return Results.Conflict(new ProblemDetails
                    {
                        Type = result.Error.Code,
                        Detail = result.Error.Description
                    });
            }

            return null!;
        }
    }
}