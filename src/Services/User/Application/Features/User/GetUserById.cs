using AutoMapper;
using FluentValidation;
using MediatR;
using User.Application.Data;
using User.Application.Dto.Output;

namespace User.Application.Features.User
{
    public class GetUserById
    {
        public record Query : IRequest<UserResponse>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
        }

        public class Handler : IRequestHandler<Query, UserResponse>
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

            public async Task<UserResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var domainUser = await _dbConext.Users.FindAsync(request.Id, cancellationToken)
                                            ?? throw new Exception("User not found.");

                return _mapper.Map<UserResponse>(domainUser);
            }
        }
    }
}