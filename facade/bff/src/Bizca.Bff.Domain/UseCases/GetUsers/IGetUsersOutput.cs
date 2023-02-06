namespace Bizca.Bff.Application.UseCases.GetUsers
{
    using Core.Domain;

    public interface IGetUsersOutput : IPublicErrorOutput
    {
        void Ok(GetPagedUsersDto pagedUsers);
    }
}