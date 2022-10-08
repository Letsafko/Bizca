namespace Bizca.User.Domain.Agregates.ValueObjects
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;

    public sealed class UserCode : ValueObject
    {
        public UserCode(Guid code)
        {
            Code = code;
        }

        public Guid Code { get; }

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