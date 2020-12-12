using Bizca.Core.Domain.Partner;

namespace Bizca.User.Domain.Agregates.Users
{
    public sealed class UserRequest
    {
        public Partner Partner { get; set; }
        public string ExternalUserId { get; set; }
    }
}