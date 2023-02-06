namespace Bizca.Core.Test.Support
{
    using Microsoft.Extensions.Logging;
    using System;

    public abstract class MockLogger<T> : ILogger<T>
    {
        void ILogger.Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
            => Log(logLevel, formatter(state, exception), exception);

        protected abstract void Log(LogLevel logLevel, object state, Exception exception = null);

        public abstract IDisposable BeginScope<TState>(TState state);

        public bool IsEnabled(LogLevel logLevel) => true;
    }
}