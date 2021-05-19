namespace Bizca.Bff.Domain.Referentials.Bundle.ValueObjects
{
    using Bizca.Core.Domain;
    public class Priority : Enumeration
    {
        public Priority(int id, string code) : base(id, code)
        {
        }

        public static readonly Priority Medium = new Priority(2, "Medium");
        public static readonly Priority High = new Priority(3, "High");
        public static readonly Priority Low = new Priority(1, "Low");
        public static Priority GetByCode(string code)
        {
            return GetFromCode<Priority>(code);
        }
        public static Priority GetById(int id)
        {
            return GetFromId<Priority>(id);
        }
    }
}