namespace Bizca.User.Infrastructure.UnitTest.Factories
{
    using Bizca.Core.Infrastructure;
    using Bizca.Core.Infrastructure.Abstracts;
    using System;

    public sealed class ConnectionFactoryBuilder
    {
        private readonly IServiceProvider provider;
        private ConnectionFactoryBuilder()
        {
            provider = NSubstitute.Substitute.For<IServiceProvider>();
        }

        public static ConnectionFactoryBuilder Instance => new ConnectionFactoryBuilder();
        public IConnectionFactory Build()
        {
            return new ConnectionFactory(provider);
        }
    }
}
