namespace Bizca.Core.Domain.Cqrs
{
    using Commands;
    using Events;
    using Queries;

    public interface IProcessor : IProcessCommand, IProcessQuery, IProcessNotification
    {
    }
}