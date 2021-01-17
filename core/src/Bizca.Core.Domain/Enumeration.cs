namespace Bizca.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Enumeration : IEquatable<Enumeration>
    {
        public int Id { get; }
        public string Code { get; }

        public Enumeration(int id, string code)
        {
            (Id, Code) = (id, code);
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return ((TypeInfo)typeof(T)).DeclaredProperties.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T GetFromName<T>(string code) where T : Enumeration
        {
            return Parse<T>(item => item.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        #region overrides

        public bool Equals(Enumeration other)
        {
            return Code.Equals(other.Code, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return obj is Enumeration o && Equals(o);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code);
        }

        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !(left == right);
        }

        public static bool operator ==(Enumeration left, Enumeration right)
        {
            return left.Equals(right);
        }

        #endregion

        #region private helpers

        private static T Parse<T>(Func<T, bool> predicate) where T : Enumeration
        {
            return GetAll<T>().FirstOrDefault(predicate);
        }

        #endregion
    }
}