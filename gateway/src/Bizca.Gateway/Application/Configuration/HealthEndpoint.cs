namespace Bizca.Gateway.Application.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    /// <summary>Health checks endpoint configuration</summary>
    public class HealthEndpoint
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the endpoint.</summary>
        /// <value>The endpoint.</value>
        public string Endpoint { get; set; }
    }
}