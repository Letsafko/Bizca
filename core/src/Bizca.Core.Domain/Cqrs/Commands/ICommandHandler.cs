namespace Bizca.Core.Domain.Cqrs.Commands
{
    using MediatR;

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }
}