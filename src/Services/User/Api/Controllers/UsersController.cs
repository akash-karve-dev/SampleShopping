using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Application.Dto.Input;
using User.Application.Dto.Output;
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

            var userId = await _mediator.Send(command);

            return Results.CreatedAtRoute("GetUserById", new { id = userId });
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IResult> GetUser([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new GetUserById.Query
            {
                Id = id
            });
            return Results.Ok<UserResponse>(user);
        }
    }
}