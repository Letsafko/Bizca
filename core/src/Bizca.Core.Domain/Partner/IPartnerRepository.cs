namespace Bizca.Core.Domain.Partner
{
    public interface IPartnerRepository
    {
        Partner GetByCode(string partnerCode);
    }
}
