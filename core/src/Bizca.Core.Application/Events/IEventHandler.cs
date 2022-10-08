namespace Bizca.Core.Application.Events
{
    using Domain;
    using MediatR;

    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}