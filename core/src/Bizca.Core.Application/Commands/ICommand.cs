namespace Bizca.Core.Application.Commands
{
    using MediatR;
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest
    {
    }
}