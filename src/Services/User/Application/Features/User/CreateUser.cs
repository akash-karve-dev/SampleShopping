using FluentValidation;
using MediatR;
using SharedKernel.ResultPattern;
using User.Application.Data;
using User.Application.Dto.Input;
using User.Domain.User;

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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserRepository _userRepository;

            public Handler(
                IUnitOfWork unitOfWork,
                IUserRepository userRepository
                )
            {
                _unitOfWork = unitOfWork;
                _userRepository = userRepository;
            }

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var domainUser = Domain.User.User.CreateUser(request.Name!, request.Email!);

                await _userRepository.AddUserAsync(domainUser, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return domainUser.Id;
            }
        }
    }
}