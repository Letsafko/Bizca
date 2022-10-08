namespace Bizca.Core.Support.Test.Builders
{
    using Domain.Referential.Model;

    public sealed class PartnerBuilder
    {
        private string _description;
        private int _id;
        private string _partnerCode;

        private PartnerBuilder()
        {
            _id = 1;
            _partnerCode = "bizca";
            _description = "bizca";
        }

        public static PartnerBuilder Instance => new PartnerBuilder();

        public Partner Build()
        {
            return new Partner(_id, _partnerCode, _description);
        }

        public PartnerBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public PartnerBuilder WithCode(string code)
        {
            _partnerCode = code;
            return this;
        }

        public PartnerBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
    }
}