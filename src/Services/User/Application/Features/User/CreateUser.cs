using AutoMapper;
using FluentValidation;
using MediatR;
using SharedKernel.ResultPattern;
using User.Application.Data;
using User.Application.Dto.Input;

namespace User.Application.Features.User
{
    public class CreateUser
    {
        public record Command : CreateUserDto, IRequest<Result<Guid>>
        {
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<Guid>>
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

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                //var userId = Guid.NewGuid();

                var domainUser = Domain.User.CreateUser(request.Name!, request.Email!);

                //domainUser.Id = userId;

                _dbConext.Users.Add(domainUser);

                await _dbConext.SaveChangesAsync();

                return domainUser.Id;
            }
        }
    }
}