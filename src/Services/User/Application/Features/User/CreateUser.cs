using FluentValidation;
using MassTransit;
using MediatR;
using SharedKernel.ResultPattern;
using SharedMessage.Events;
using User.Application.Abstractions;
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
                RuleFor(x => x.Email).EmailAddress();
            }
        }

        public class Handler(IUnitOfWork unitOfWork,
                             IUserRepository userRepository,
                             IBus bus) : IRequestHandler<Command, Result<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork;
            private readonly IUserRepository _userRepository = userRepository;
            private readonly IBus _bus = bus;

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var domainUser = Domain.User.User.CreateUser(request.Name!, request.Email!);

                if (await _userRepository.IsUserExistAsync(request.Name!, request.Email!, cancellationToken))
                {
                    return Result.Failure<Guid>(UserErrors.AlreadyExist(domainUser.Id));
                }

                await _userRepository.AddUserAsync(domainUser, cancellationToken);

                await _bus.Publish(new UserCreatedEvent
                {
                    Id = domainUser.Id,
                    Email = domainUser.Email,
                    Name = domainUser.Name
                }, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return domainUser.Id;
            }
        }
    }
}