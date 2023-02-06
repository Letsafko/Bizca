namespace Bizca.Core.Domain.Cqrs.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IProcessQuery
    {
        Task ProcessQueryAsync(IQuery query, CancellationToken cancellationToken);
    }
}