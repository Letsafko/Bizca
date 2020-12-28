namespace Bizca.User.Application.UseCases.GetUser.List
{
    using Bizca.Core.Application.Abstracts;
    using Bizca.Core.Application.Abstracts.Paging;
    using Bizca.Core.Application.Abstracts.Queries;
    using Newtonsoft.Json;
    using System;

    public sealed class GetUsersQuery : Paged, IQuery, ICloneable
    {
        public GetUsersQuery(string partnerCode, string pageSize, string pageNumber, string direction, string birthdate)
        {
            if (string.IsNullOrWhiteSpace(partnerCode))
            {
                ModelState.Add(nameof(partnerCode), $"{nameof(partnerCode)} is required.");
            }
            else
            {
                PartnerCode = partnerCode;
            }

            if (string.IsNullOrWhiteSpace(pageSize))
            {
                ModelState.Add(nameof(pageSize), $"{nameof(pageSize)} is required.");
            }
            else if (int.TryParse(pageSize, out int size))
            {
                PageSize = size;
            }
            else
            {
                ModelState.Add(nameof(pageSize), $"{nameof(pageSize)} is invalid.");
            }

            if (string.IsNullOrWhiteSpace(pageNumber))
            {
                ModelState.Add(nameof(pageNumber), $"{nameof(pageNumber)} is required.");
            }
            else if (int.TryParse(pageNumber, out int number))
            {
                PageNumber = number;
            }
            else
            {
                ModelState.Add(nameof(pageNumber), $"{nameof(pageNumber)} is invalid.");
            }

            if (string.IsNullOrWhiteSpace(direction))
            {
                ModelState.Add(nameof(direction), $"{nameof(direction)} is required.");
            }
            else if (!direction.Equals(SearchDirection.Next, StringComparison.OrdinalIgnoreCase) &&
                     !direction.Equals(SearchDirection.Previous, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.Add(nameof(direction), $"{nameof(direction)} value should be next or previous.");
            }
            else
            {
                Direction = direction;
            }

            if (!string.IsNullOrWhiteSpace(birthdate))
            {
                if(DateTime.TryParse(birthdate, out DateTime birthday))
                    BirthDate = birthday.ToString("yyyy-MM-dd");
                else
                    ModelState.Add(nameof(birthdate), $"{nameof(birthdate)} value is invalid.");
            }
        }

        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PartnerCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Whatsapp { get; set; }
        public string ExternalUserId { get; set; }
        public string BirthDate { get; set; }

        [JsonIgnore]
        public string RequestPath { get; set; }

        [JsonIgnore]
        public Notification ModelState { get; } = new Notification();

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    public static class SearchDirection
    {
        public const string Next = "next";
        public const string Previous = "previous";
    }
}