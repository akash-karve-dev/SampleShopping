using AutoMapper;
using FluentValidation;
using MediatR;
using User.Application.Data;
using User.Application.Dto.Input;

namespace User.Application.Features.User
{
    public class CreateUser
    {
        public record Command : CreateUserDto, IRequest<Guid>
        {
        }

        public class Validator : AbstractValidator<Command>
        { }

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IApplicationDbConext _dbConext;
            private readonly IMapper _mapper;

            public Handler(
                IApplicationDbConext dbConext,
                IMapper mapper
                )
            {
                _dbConext = dbConext;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = Guid.NewGuid();

                var domainUser = _mapper.Map<Domain.User>(request);

                domainUser.Id = userId;

                _dbConext.Users.Add(domainUser);

                await _dbConext.SaveChangesAsync();

                return userId;
            }
        }
    }
}