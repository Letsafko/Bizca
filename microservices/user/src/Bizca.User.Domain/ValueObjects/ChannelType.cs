namespace Bizca.User.Domain.ValueObjects
{
    using Bizca.Core.Domain;

    public sealed class ChannelType : Enumeration
    {
        public ChannelType(int id, string code) : base(id, code)
        {
        }

        public static readonly ChannelType Sms = new ChannelType(1, "Sms");
        public static readonly ChannelType Email = new ChannelType(2, "Email");
        public static readonly ChannelType Whatsapp = new ChannelType(4, "Whatsapp");
        public static readonly ChannelType Messenger = new ChannelType(8, "Messenger");
        public static ChannelType GetByCode(string code)
        {
            return GetFromName<ChannelType>(code) ?? ChannelType.Email;
        }
    }
}