namespace Bizca.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class Enumeration : IEquatable<Enumeration>
    {
        public int Id { get; }
        public string Code { get; }

        protected Enumeration(int id, string code)
        {
            (Id, Code) = (id, code);
        }

        protected static T GetFromCode<T>(string code) where T : Enumeration
        {
            return Parse<T>(item => item.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        protected static T GetFromId<T>(int id) where T : Enumeration
        {
            return Parse<T>(item => item.Id == id);
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

        private static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                            .Select(f => f.GetValue(null))
                            .Cast<T>();
        }

        #endregion
    }
}