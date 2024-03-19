using AutoMapper;
using FluentValidation;
using MediatR;
using SharedKernel.ResultPattern;
using User.Application.Data;
using User.Application.Dto.Output;
using User.Domain;

namespace User.Application.Features.User
{
    public class GetUserById
    {
        public record Query : IRequest<Result<UserResponse>>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
        }

        public class Handler : IRequestHandler<Query, Result<UserResponse>>
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

            public async Task<Result<UserResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var domainUser = await _dbConext.Users.FindAsync(request.Id, cancellationToken);

                if (domainUser == null)
                {
                    return Result.Failure<UserResponse>(UserErrors.NotFound(request.Id));
                }

                var response = _mapper.Map<UserResponse>(domainUser);

                return response;
            }
        }
    }
}