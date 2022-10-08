namespace Bizca.User.Domain
{
    using Core.Domain;

    public sealed class ChannelType : Enumeration<int>
    {
        public static readonly ChannelType Messenger = new ChannelType(8, "Messenger");
        public static readonly ChannelType Whatsapp = new ChannelType(4, "Whatsapp");
        public static readonly ChannelType Email = new ChannelType(2, "Email");
        public static readonly ChannelType Sms = new ChannelType(1, "Sms");

        private ChannelType(int code, string label) : base(code, label)
        {
        }

        public static ChannelType GetByCode(int code)
        {
            return GetFromCode<ChannelType>(code);
        }

        public static ChannelType GetByLabel(string label)
        {
            return GetFromLabel<ChannelType>(label);
        }
    }
}