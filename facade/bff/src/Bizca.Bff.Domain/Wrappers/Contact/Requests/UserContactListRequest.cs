namespace Bizca.Bff.Domain.Wrappers.Contact.Requests
{
    public sealed class UserContactListRequest
    {
        public UserContactListRequest(string name, int folderId)
        {
            FolderId = folderId;
            Name = name;
        }

        public string Name { get; }
        public int FolderId { get; }
    }
}