namespace Bizca.User.Domain.Entities.Address.Factories
{
    using System.Threading.Tasks;
    public interface IAddressFactory
    {
        Task<Address> CreateAsync(AddressRequest request);
    }
}