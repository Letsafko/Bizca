namespace Bizca.Bff.Application.UseCases.GetUserDetails
{
    using Core.Domain;

    public interface IGetUserDetailsOutput : IPublicErrorOutput
    {
        void Ok(GetUserDetailsDto detailsDto);
    }
}