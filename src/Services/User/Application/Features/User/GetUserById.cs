using AutoMapper;
using FluentValidation;
using MediatR;
using SharedKernel.ResultPattern;
using User.Application.Dto.Output;
using User.Domain.User;

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

        public class Handler(IUserRepository userRepository,
                             IMapper mapper) : IRequestHandler<Query, Result<UserResponse>>
        {
            private readonly IUserRepository _userRepository = userRepository;
            private readonly IMapper _mapper = mapper;

            public async Task<Result<UserResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var domainUser = await _userRepository.GetByIdAync(request.Id, cancellationToken);

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