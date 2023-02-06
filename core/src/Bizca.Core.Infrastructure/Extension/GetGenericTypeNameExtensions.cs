namespace Bizca.Core.Infrastructure.Extension
{
    using System;
    using System.Text;

    public static class GetGenericTypeNameExtensions
    {
        public static string GetGenericTypeName(this object obj)
        {
            return obj
                .GetType()
                .GetGenericTypeName();
        }

        public static string GetGenericTypeName(this Type type)
        {
            return type.GetRecursiveTypeName();
        }

        private static string GetRecursiveTypeName(this Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            var argumentTypes = type.GetGenericArguments();
            var typeBuilder = new StringBuilder($"{type.Name.Remove(type.Name.IndexOf('`'))}<");
            foreach (var argumentType in argumentTypes)
            {
                if (!argumentType.IsGenericType)
                {
                    typeBuilder.Append($"{argumentType.Name},");
                    continue;
                }

                typeBuilder.Append($"{argumentType.GetRecursiveTypeName()},");
            }

            var typeName = typeBuilder.ToString();
            return typeName.Remove(typeName.LastIndexOf(',')) + ">";
        }
    }
}