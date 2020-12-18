namespace Bizca.Core.Domain.Civility
{
    public sealed class Civility
    {
        public Civility(int civilityId, string civilityCode)
        {
            CivilityId = civilityId;
            CivilityCode = civilityCode;
        }

        public int CivilityId { get; }
        public string CivilityCode { get; }
    }
}
