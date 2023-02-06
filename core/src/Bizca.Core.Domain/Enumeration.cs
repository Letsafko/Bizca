namespace Bizca.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class Enumeration<TIdentifier> : ValueObject 
    {
        protected Enumeration(TIdentifier code, string label) => (Code, Label) = (code, label);

        public TIdentifier Code { get; }
        public string Label { get; }

        protected static T GetFromCode<T>(TIdentifier code) where T : Enumeration<TIdentifier>
        {
            return Parse<T>(item => Equals(item.Code, code));
        }
    
        protected static T GetFromLabel<T>(string label) where T : Enumeration<TIdentifier>
        {
            return Parse<T>(item => item.Label.Equals(label, StringComparison.OrdinalIgnoreCase));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Label;
            yield return Code;
        }

        private static T Parse<T>(Func<T, bool> predicate) where T : Enumeration<TIdentifier> 
        {
            return GetAll<T>().SingleOrDefault(predicate);
        }

        private static IEnumerable<T> GetAll<T>() where T : Enumeration<TIdentifier>
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();
        }
    }
}