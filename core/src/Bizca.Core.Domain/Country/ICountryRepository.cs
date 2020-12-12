namespace Bizca.Core.Domain.Country
{
    public interface ICountryRepository
    {
        Country GetByCode(string code);
    }
}
