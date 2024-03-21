using MediatR;

namespace Template.Application.Core.Abstractions.Messages
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
