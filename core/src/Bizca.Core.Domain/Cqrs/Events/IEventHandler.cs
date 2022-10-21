namespace Bizca.Core.Domain.Cqrs.Events
{
    using MediatR;

    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : INotificationEvent
    {
    }
}