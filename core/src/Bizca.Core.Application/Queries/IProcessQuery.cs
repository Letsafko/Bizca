namespace Bizca.Core.Application.Queries
{
    using System.Threading.Tasks;

    public interface IProcessQuery
    {
        Task ProcessQueryAsync(IQuery query);
        Task<TResult> ProcessQueryAsync<TResult>(IQuery<TResult> query);
    }
}