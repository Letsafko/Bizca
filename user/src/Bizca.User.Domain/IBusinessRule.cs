namespace Bizca.User.Domain
{
    using System.Threading.Tasks;

    public interface IBusinessRule<in TRequest>
    {
        Task<bool> CheckAsync(TRequest request);
    }
}
