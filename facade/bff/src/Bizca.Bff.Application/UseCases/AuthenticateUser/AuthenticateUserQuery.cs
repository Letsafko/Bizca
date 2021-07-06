namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using Bizca.Core.Application.Queries;
    public sealed class AuthenticateUserQuery : IQuery
    {
        public AuthenticateUserQuery(string password, string channelResource)
        {
            Resource = channelResource;
            Password = password;
        }

        public string Resource { get; }
        public string Password { get; }
    }
}
