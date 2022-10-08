﻿namespace Bizca.Core.Application.Events
{
    using Domain;
    using System.Threading.Tasks;

    public interface IProcessNotification
    {
        Task ProcessNotificationAsync(IEvent @event);
    }
}