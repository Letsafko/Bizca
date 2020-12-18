namespace Bizca.User.WebApi.ViewModels
{
    using Bizca.User.Application.UseCases.GetUserDetail;
    using System.Collections.Generic;

    public sealed class UserDetailsModel
    {
        public UserDetailsModel(GetUserDetailDto userDetail)
        {
            Email = userDetail.Email;
            UserCode = userDetail.UserCode;
            Civility = userDetail.Civility;
            LastName = userDetail.LastName;
            Channels = userDetail.Channels;
            FirstName = userDetail.FirstName;
            BirthCity = userDetail.BirthCity;
            BirthDate = userDetail.BirthDate;
            PhoneNumber = userDetail.PhoneNumber;
            BirthCountry = userDetail.BirthCountry;
            ExternalUserId = userDetail.ExternalUserId;
            EconomicActivity = userDetail.EconomicActivity;
        }

        public string UserCode { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string Civility { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public string BirthCity { get; }
        public string BirthDate { get; }
        public string BirthCountry { get; }
        public string ExternalUserId { get; }
        public string EconomicActivity { get; }
        public IEnumerable<string> Channels { get; }
    }
}
