namespace Bizca.User.Domain.Agregates.Users
{
    using Bizca.Core.Domain;
    using System;
    using System.Collections.Generic;

    public sealed class UserCode : ValueObject
    {
        public Guid Code { get; }
        public UserCode(Guid code)
        {
            Code = code;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
        }
    }
}
