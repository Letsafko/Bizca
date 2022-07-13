namespace Bizca.Core.Domain.Test.Services
{
    using Bizca.Core.Domain.Services;
    using Shouldly;
    using System;
    using Xunit;

    public sealed class DateServiceTest
    {
        [Fact]
        public void Should_Return_UtcNow()
        {
            //Arrange
            var dateService = new DateService();

            //Act
            var now = dateService.Now;

            //Assert
            now.ShouldBe(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void Should_format_Date_to_String()
        {
            //Arrange
            var dateService = new DateService();
            DateTime now = new DateTime(2008, 10, 31, 17, 4, 32);
            const string expected = "2008-10-31T17:04:32";

            //Act
            var dateTimeToString = dateService.DateToString(now);

            //Assert
            dateTimeToString.ShouldBe(expected);
        }
    }
}