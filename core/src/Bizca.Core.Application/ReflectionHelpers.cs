namespace Bizca.Core.Application
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    public static class ReflectionHelpers
    {
        private const BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;
        public static IDictionary<string, string> Populate<T>(T source, BindingFlags bindingAttr = bindingFlags) where T : class
        {
            var dicoProperties = new Dictionary<string, string>();
            if (source is null)
            {
                return dicoProperties;
            }

            foreach (PropertyInfo propInfo in source.GetType().GetProperties(bindingAttr))
            {
                dicoProperties[propInfo.Name] = IsSimple(propInfo.GetType())
                    ? propInfo.GetValue(source, null)?.ToString()
                    : JsonConvert.SerializeObject(propInfo.GetValue(source, null), Formatting.Indented);
            }

            return dicoProperties;
        }
        private static bool IsSimple(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? IsSimple(type.GetGenericArguments()[0])
                : type.Equals(typeof(decimal)) ||
                  type.Equals(typeof(string)) ||
                  type.IsPrimitive ||
                  type.IsEnum;
        }
    }
}