namespace Bizca.Bff.Application.UseCases.GetUsers
{
    public interface IGetUsersOutput
    {
        void Ok(GetPagedUsersDto pagedUsers);
    }
}