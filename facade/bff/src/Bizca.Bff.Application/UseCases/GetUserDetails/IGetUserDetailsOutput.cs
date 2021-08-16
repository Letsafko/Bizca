namespace Bizca.Bff.Application.UseCases.GetUserDetails
{
    public interface IGetUserDetailsOutput
    {
        void Ok(GetUserDetailsDto detailsDto);
    }
}
