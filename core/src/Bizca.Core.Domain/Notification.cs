namespace Bizca.Core.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Notification
    {
        public bool IsValid => _errorMessages.Count == 0;
        public IDictionary<string, string[]> Errors => _errorMessages.ToDictionary(item => item.Key, item => item.Value.ToArray());
        public void Add(string key, string message)
        {
            key = key.ToLower();
            if (!_errorMessages.ContainsKey(key))
            {
                _errorMessages[key] = new List<string>();
            }

            _errorMessages[key].Add(message);
        }

        public void Add(KeyValuePair<string, string[]> error)
        {
            string key = error.Key.ToLower();
            if (!_errorMessages.ContainsKey(key))
            {
                _errorMessages[key] = new List<string>();
            }

            _errorMessages[key].AddRange(error.Value);
        }

        public void Add(IDictionary<string, string[]> errors)
        {
            foreach(KeyValuePair<string, string[]> err in errors)
            {
                Add(err);
            }
        }

        private readonly IDictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();
    }
}
