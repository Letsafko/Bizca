namespace Bizca.Core.Domain.Cqrs.Queries
{
    using MediatR;

    public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery> where TQuery : IQuery
    {
    }
}