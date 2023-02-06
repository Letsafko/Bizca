namespace Bizca.Core.Test.Support.Builder
{
    using System.Collections.Generic;
    using System.Dynamic;

    public class DynamicDictionary : DynamicObject
    {
        private readonly Dictionary<string, object> _dictionary = new();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();
            return _dictionary.TryGetValue(name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dictionary[binder.Name.ToLower()] = value;
            return true;
        }
    }
}