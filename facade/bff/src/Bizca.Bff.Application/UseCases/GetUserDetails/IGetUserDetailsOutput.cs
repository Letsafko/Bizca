namespace Bizca.Bff.Application.UseCases.GetUserDetails
{
    using Bizca.Core.Domain;
    public interface IGetUserDetailsOutput : IPublicErrorOutput
    {
        void Ok(GetUserDetailsDto detailsDto);
    }
}
