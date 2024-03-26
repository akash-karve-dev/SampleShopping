using MediatR;
using SharedKernel.ResultPattern;

namespace SharedKernel.Messaging
{
    public interface ICommand : IRequest<Result>
    { }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    { }

   
}