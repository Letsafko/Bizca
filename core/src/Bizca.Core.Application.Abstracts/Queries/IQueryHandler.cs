namespace Bizca.Core.Application.Abstracts.Queries
{
    using MediatR;
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}
