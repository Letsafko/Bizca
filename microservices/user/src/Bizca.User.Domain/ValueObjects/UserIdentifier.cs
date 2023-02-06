namespace Bizca.User.Domain.Agregates;

using Core.Domain;
using Core.Domain.Referential.Model;
using System.Collections.Generic;
using ValueObjects;

public sealed class UserIdentifier : ValueObject
{
    public UserIdentifier(int userId, ExternalUserId externalUserId, PublicUserCode publicUserCode, Partner partner)
    {
        ExternalUserId = externalUserId;
        PublicUserCode = publicUserCode;
        Partner = partner;
        UserId = userId;
    }
    
    public ExternalUserId ExternalUserId { get; }
    public PublicUserCode PublicUserCode { get; }
    public Partner Partner { get; }
    public int UserId { get; }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return ExternalUserId;
        yield return PublicUserCode;
        yield return Partner;
        yield return UserId;
    }
}