namespace Bizca.User.Application.UseCases;

public sealed class AddressCommand
{
    public string Name { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}