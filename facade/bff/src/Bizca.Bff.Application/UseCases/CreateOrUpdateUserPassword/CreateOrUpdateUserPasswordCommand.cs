namespace Bizca.Bff.Application.UseCases.CreateOrUpdateUserPassword
{
    using Bizca.Core.Application.Commands;
    public sealed class CreateOrUpdateUserPasswordCommand : ICommand
    {
        public CreateOrUpdateUserPasswordCommand(string password, string resource)
        {
            Password = password;
            Resource = resource;
        }

        public string Password { get; }
        public string Resource { get; }
    }
}