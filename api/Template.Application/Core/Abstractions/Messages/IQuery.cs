using MediatR;

namespace Template.Application.Core.Abstractions.Messages
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
