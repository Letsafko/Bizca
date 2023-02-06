namespace Bizca.User.Domain.BusinessCheck.AddressRule.Contract
{
    using Core.Domain.Rules;
    using Entities.Address;

    public interface IAddressRule : IBusinessRule<AddressRequest>
    {
    }
}