namespace Bizca.User.Domain.Agregates.BusinessCheck
{
    using Core.Domain.Rules;

    public interface IUserRuleEngine : IBusinessRuleEngine<UserRequest>
    {
    }
}