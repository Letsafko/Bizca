namespace Bizca.Core.Domain.Services
{
    using System;
    using System.Globalization;

    /// <summary>
    /// <see cref="https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=net-6.0"/>
    /// </summary>
    public sealed class DateService : IDateService
    {
        public DateTime Now => DateTime.UtcNow;

        public string DateToString(DateTime date, string format = "s", string culture = "fr-FR")
        {
            return date.ToString(format,
                CultureInfo.CreateSpecificCulture(culture));
        }
    }
}