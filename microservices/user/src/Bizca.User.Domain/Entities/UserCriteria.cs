namespace Bizca.User.Domain.Agregates;

public sealed class UserCriteria
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string WhatsappNumber { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string ExternalUserId { get; set; }
    public string BirthDate { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; } = 0;
    public string Direction { get; set; }
}