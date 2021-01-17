namespace Bizca.Gateway.Application.Extensions
{
    using Bizca.Gateway.Application.Configuration;
    using System.Collections.Generic;

    internal static class RouteExtensions
    {
        private static readonly List<string> routeKeys = new List<string>();

        internal static bool Validate(this ExtendedFileRoute route)
        {
            string routeKey = $"{route.UpstreamPathTemplate}-{string.Join(',', route.UpstreamHttpMethod.ToArray())}";

            if (route.DownstreamHostAndPorts.Count == 0)
            {
                route.DownstreamHostAndPorts.Add(new Ocelot.Configuration.File.FileHostAndPort()
                {
                    Host = "ERROR_HOST_EMPTY"
                });

                return false;
            }
            else if (string.IsNullOrEmpty(route.DownstreamHostAndPorts[0].Host))
            {
                route.DownstreamHostAndPorts[0].Host = "ERROR_HOST_EMPTY";

                return false;
            }
            else if (!route.DownstreamPathTemplate.StartsWith('/'))
            {
                route.DownstreamPathTemplate = "ERROR_DONT_START_WITH_SLASH";

                return false;
            }
            else if (routeKeys.Contains(routeKey))
            {
                route.UpstreamPathTemplate = "ERROR_DUPLICATE_ROUTE";

                return false;
            }
            else if (route.AuthenticationOptions?.AuthenticationProviderKey == Constants.IDENTITY_PROVIDER_KEY
                && (route.AuthenticationOptions.AllowedScopes.Count != 1
                    || (route.AuthenticationOptions.AllowedScopes.Count == 1 && string.IsNullOrEmpty(route.AuthenticationOptions.AllowedScopes[0]))))
            {
                route.AuthenticationOptions.AuthenticationProviderKey = "ERROR_ONE_ALLOWED_SCOPED_MUST_BE_SET";

                return false;
            }

            routeKeys.Add(routeKey);

            return true;
        }
    }
}