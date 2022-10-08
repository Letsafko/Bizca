namespace Bizca.Core.Application
{
    using Commands;
    using Events;
    using Queries;

    public interface IProcessor : IProcessCommand, IProcessQuery, IProcessNotification
    {
    }
}