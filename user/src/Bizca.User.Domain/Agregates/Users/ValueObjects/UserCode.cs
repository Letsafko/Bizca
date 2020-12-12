namespace Bizca.User.Domain.Agregates.Users.ValueObjects
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

        public override string ToString()
        {
            return Code.ToString();
        }
    }
}
