namespace Bizca.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class Enumeration<TIdentifier> : ValueObject
    {
        protected Enumeration(TIdentifier code, string label, string description = "")
        {
            if (string.IsNullOrWhiteSpace(description)) description = label;

            (Code, Label, Description) = (code, label, description);
        }

        public TIdentifier Code { get; }
        public string Label { get; }
        public string Description { get; }

        protected static T GetFromCode<T>(TIdentifier code) where T : Enumeration<TIdentifier>
        {
            return Parse<T>(item => item.Code.ToString().ToLower().Equals(code.ToString().ToLower()));
        }

        protected static T GetFromLabel<T>(string label) where T : Enumeration<TIdentifier>
        {
            return Parse<T>(item => item.Label.Equals(label, StringComparison.OrdinalIgnoreCase));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code.ToString().ToLower();
            yield return Description?.ToLower();
            yield return Label.ToLower();
        }

        #region private helpers

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

        #endregion
    }
}