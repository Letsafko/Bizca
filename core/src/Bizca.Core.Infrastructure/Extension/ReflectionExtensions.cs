namespace Bizca.Core.Infrastructure.Extension
{
    using Domain.Attribute;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionExtensions
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.DeclaredOnly |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance;

        public static IDictionary<string, string> Crumble<T>(this T source, BindingFlags bindingFlags = BindingFlags)
            where T : class
        {
            var properties = new Dictionary<string, string>();
            if (source is null) return properties;

            foreach (PropertyInfo propInfo in source.GetFilteredProperties(bindingFlags))
                properties[propInfo.Name] = IsSimple(propInfo.GetType())
                    ? propInfo.GetValue(source, null)?.ToString()
                    : JsonConvert.SerializeObject(propInfo.GetValue(source, null), Formatting.Indented);

            return properties;
        }

        private static bool IsSimple(Type type)
        {
            while (true)
            {
                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>))
                    return type == typeof(decimal) ||
                           type == typeof(string) ||
                           type.IsPrimitive ||
                           type.IsEnum;

                type = type.GetGenericArguments()[0];
            }
        }

        private static IEnumerable<PropertyInfo> GetFilteredProperties(this object source, BindingFlags bindingFlags)
        {
            return source
                ?.GetType()
                .GetProperties(bindingFlags)
                .Where(pi => !Attribute.IsDefined(pi, typeof(SkipPropertyAttribute)))
                .ToArray();
        }
    }
}