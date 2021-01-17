namespace Bizca.Core.Application.Abstracts
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

        private readonly IDictionary<string, IList<string>> _errorMessages = new Dictionary<string, IList<string>>();
    }
}
