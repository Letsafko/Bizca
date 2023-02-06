namespace Bizca.User.Domain.BusinessCheck.UserRule.Contract
{
    using Agregates;
    using Core.Domain.Rules;

    public interface IUserRule : IBusinessRule<UserRequest>
    {
    }
}