namespace Bizca.Bff.Domain.Wrappers.Contact.Responses
{
    public sealed class CreateContactResponse
    {
        public int FolderId { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}