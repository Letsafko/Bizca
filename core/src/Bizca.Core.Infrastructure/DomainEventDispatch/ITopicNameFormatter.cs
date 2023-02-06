namespace Bizca.Core.Infrastructure.DomainEventDispatch
{
    using System;

    public interface ITopicNameFormatter
    {
        string Get(Type type);
    }
}