namespace Bizca.Core.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    public sealed class Notification
    {
        public bool IsValid => errorMessages.Count == 0;
        public IReadOnlyDictionary<string, string[]> Errors => errorMessages.ToDictionary(item => item.Key, item => item.Value.ToArray());
        private readonly IDictionary<string, List<string>> errorMessages = new Dictionary<string, List<string>>();

        public void Add(IDictionary<string, string[]> errors)
        {
            foreach (KeyValuePair<string, string[]> err in errors)
            {
                Add(err);
            }
        }
        public void Add(KeyValuePair<string, string[]> error)
        {
            string key = error.Key.ToLower();
            if (!errorMessages.ContainsKey(key))
            {
                errorMessages[key] = new List<string>();
            }

            errorMessages[key].AddRange(error.Value);
        }
        public void Add(string key, string message)
        {
            key = key.ToLower();
            if (!errorMessages.ContainsKey(key))
            {
                errorMessages[key] = new List<string>();
            }

            errorMessages[key].Add(message);
        }

    }
}