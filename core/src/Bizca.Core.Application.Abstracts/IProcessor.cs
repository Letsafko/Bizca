namespace Bizca.Core.Application.Abstracts
{
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Application.Abstracts.Notifications;
    using Bizca.Core.Application.Abstracts.Queries;

    public interface IProcessor : IProcessCommand, IProcessQuery, IProcessNotification
    {
    }
}
