namespace Bizca.Core.Application.Abstracts.Queries
{
    using MediatR;
    public interface IQuery : IRequest
    {
    }
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
