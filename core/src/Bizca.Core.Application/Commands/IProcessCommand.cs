namespace Bizca.Core.Application.Commands
{
    using System.Threading.Tasks;
    public interface IProcessCommand
    {
        Task ProcessCommandAsync(ICommand command);
        Task<TResult> ProcessCommandAsync<TResult>(ICommand<TResult> command);
    }
}