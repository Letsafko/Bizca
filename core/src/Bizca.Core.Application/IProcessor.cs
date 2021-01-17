namespace Bizca.Core.Application
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Application.Queries;

    public interface IProcessor : IProcessCommand, IProcessQuery, IProcessNotification
    {
    }
}