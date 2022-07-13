using System.Collections.Generic;

namespace Bizca.Bff.Domain
{
    public static class DictionaryExtensions
    {
        public static void AddNewPair(this IDictionary<string, object> dico,
            string key,
            object value)
        {
            dico ??= new Dictionary<string, object>();
            dico[key] = value;
        }
    }
}
