namespace Bizca.Core.Application.Abstracts.Commands
{
    using MediatR;
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
