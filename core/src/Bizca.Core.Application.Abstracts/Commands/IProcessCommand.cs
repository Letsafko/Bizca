namespace Bizca.Core.Application.Abstracts.Commands
{
    using System.Threading.Tasks;
    public interface IProcessCommand
    {
        Task ProcessCommandAsync(ICommand command);
        Task ProcessCommandAsync<TResult>(ICommand<TResult> command);
    }
}
