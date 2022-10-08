namespace Bizca.Bff.Domain.Referentials.Bundle.ValueObjects
{
    using Core.Domain;

    public class Priority : Enumeration<int>
    {
        public static readonly Priority Medium = new Priority(2, "Medium");
        public static readonly Priority High = new Priority(3, "High");
        public static readonly Priority Low = new Priority(1, "Low");

        private Priority(int code, string label) : base(code, label)
        {
        }

        public static Priority GetByCode(int code)
        {
            return GetFromCode<Priority>(code);
        }
    }
}