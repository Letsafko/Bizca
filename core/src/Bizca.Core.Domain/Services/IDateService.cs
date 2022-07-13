namespace Bizca.Core.Domain.Services
{
    using System;

    public interface IDateService
    {
        DateTime Now { get; }
        string DateToString(DateTime date);
    }
}