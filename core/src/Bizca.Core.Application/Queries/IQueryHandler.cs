namespace Bizca.Core.Application.Queries
{
    using MediatR;

    public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery> where TQuery : IQuery
    {
    }

    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}