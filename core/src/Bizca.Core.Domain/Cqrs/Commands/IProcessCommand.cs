namespace Bizca.Core.Domain.Cqrs.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IProcessCommand
    {
        Task ProcessCommandAsync(ICommand command, CancellationToken cancellationToken);
    }
}