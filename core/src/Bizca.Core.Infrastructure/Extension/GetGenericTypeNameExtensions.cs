namespace Bizca.Core.Infrastructure.Extension
{
    using System;
    using System.Linq;

    public static class GetGenericTypeNameExtensions
    {
        public static string GetGenericTypeName(this object obj)
        {
            Type type = obj.GetType();
            return type.GetGenericTypeName();
        }

        public static string GetGenericTypeName(this Type type)
        {
            return !type.IsGenericType
                ? type.Name
                : type.GetNameForGenericType();
        }

        private static string GetNameForGenericType(this Type type)
        {
            string genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
            return $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
        }
    }
}