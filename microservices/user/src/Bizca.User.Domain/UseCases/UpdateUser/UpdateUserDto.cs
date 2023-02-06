namespace Bizca.User.Application.UseCases.UpdateUser;

using Domain.Entities.Address;
using Domain.Entities.Channel;
using System.Collections.Generic;

public sealed class UpdateUserDto
{
    public string UserCode { get; set; }
    public string ExternalUserId { get; set; }
    public string Civility { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string BirthCity { get; set; }
    public string BirthDate { get; set; }
    public string BirthCountry { get; set; }
    public string EconomicActivity { get; set; }
    public Address Address { get; set; }
    public List<Channel> Channels { get; set; }
}