namespace Bizca.Bff.Application.UseCases.ReInitializedPassword
{
    using Bizca.Core.Application.Commands;
    public sealed class ReInitializedPasswordCommand : ICommand
    {
        public ReInitializedPasswordCommand(string partnerCode, string email)
        {
            PartnerCode = partnerCode;
            Email = email;
        }

        public string PartnerCode { get; }
        public string Email { get; }
    }
}
