namespace Bizca.Core.Domain.EconomicActivity
{
    public class EconomicActivity : Entity
    {
        public string Description { get; }
        public string EconomicActivityCode { get; }
        public EconomicActivity(int id, string economicActivityCode, string description)
        {
            Id = id;
            Description = description;
            EconomicActivityCode = economicActivityCode;
        }
    }
}