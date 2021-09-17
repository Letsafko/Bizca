namespace Bizca.Core.Domain
{
    public interface IPublicErrorOutput
    {
        void Invalid(IPublicResponse response);
    }
}