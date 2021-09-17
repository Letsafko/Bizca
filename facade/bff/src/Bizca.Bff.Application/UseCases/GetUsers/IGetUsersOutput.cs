namespace Bizca.Bff.Application.UseCases.GetUsers
{
    using Bizca.Core.Domain;
    public interface IGetUsersOutput : IPublicErrorOutput
    {
        void Ok(GetPagedUsersDto pagedUsers);
    }
}