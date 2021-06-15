namespace Bizca.Core.Application.Events
{
    using Bizca.Core.Domain;
    using MediatR;
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}