using MediatR;
using SharedKernel.ResultPattern;

namespace SharedKernel.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    { }
}