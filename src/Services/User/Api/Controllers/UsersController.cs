using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.ResultPattern;
using User.Application.Dto.Input;
using User.Application.Features.User;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var command = new CreateUser.Command
            {
                Email = createUserDto.Email,
                Name = createUserDto.Name
            };

            var result = await _mediator.Send(command);

            return Results.CreatedAtRoute("GetUserById", new { id = result.Value });
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IResult> GetUser([FromRoute] Guid id)
        {

            throw new ArgumentException();

            var result = await _mediator.Send(new GetUserById.Query
            {
                Id = id
            });
            return result.IsSuccess ? Results.Ok(result.Value) : HandlerError(result);
        }

        // TODO : Handle in elegant way
        private IResult HandlerError(Result result)
        {
            switch (result.Error.Code)
            {
                case "User.NotFound":
                    return Results.NotFound(result.Error.Description);
            }
            return null;
        }
    }
}