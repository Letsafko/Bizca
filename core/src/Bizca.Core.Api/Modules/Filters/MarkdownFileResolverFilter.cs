namespace Bizca.Core.Api.Modules.Filters
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public sealed class MarkdownFileResolverFilter : IDocumentFilter
    {
        #region Fields & Constants

        private const string SwaggerIntroductionMarkdownFilePath = "Assets/Documentation/Introduction.md";
        private readonly string _microserviceAssemblyLocation = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;

        #endregion

        #region Methods

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!Directory.Exists(_microserviceAssemblyLocation))
            {
                return;
            }

            GenerateIntroduction(swaggerDoc);
            foreach (System.Collections.Generic.KeyValuePair<string, OpenApiPathItem> path in swaggerDoc.Paths)
            {
                foreach (System.Collections.Generic.KeyValuePair<OperationType, OpenApiOperation> operation in path.Value.Operations)
                {
                    GenerateDescription(operation.Value);
                }
            }
        }

        /// <summary>
        /// Generates the Swagger document introduction.
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        private void GenerateIntroduction(OpenApiDocument swaggerDoc)
        {
            string markdown = ReadMarkdownFile(SwaggerIntroductionMarkdownFilePath);
            if (!string.IsNullOrEmpty(markdown))
            {
                swaggerDoc.Info.Description = markdown;
            }
        }

        /// <summary>
        /// Detects if the description field of an operation targets a markdown file (.MD file) and try set the file content as the markdown description of the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <example>
        /// In a endpoint, defines the "/// <remarks></remarks>" header and put a relative path
        /// to the .MD file. Example : "/// <remarks>/Assets/MyFile.md</remarks>"
        /// </example>
        private void GenerateDescription(OpenApiOperation operation)
        {
            if (operation is null)
            {
                return;
            }

            string markdown = ReadMarkdownFile(operation.Description);
            if (!string.IsNullOrEmpty(markdown))
            {
                operation.Description = markdown;
            }
        }

        /// <summary>
        /// Reads a markdown file.
        /// </summary>
        /// <param name="relativeFilePath">The relative path to the markdown file.</param>
        /// <returns>Returns an empty string if the file does not exist.</returns>
        private string ReadMarkdownFile(string relativeFilePath)
        {
            if (string.IsNullOrWhiteSpace(relativeFilePath) || !relativeFilePath.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            string mdFileLocation = relativeFilePath.TrimStart('/').TrimStart('\\');
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                mdFileLocation = mdFileLocation.Replace("/", @"\");
            }
            else
            {
                mdFileLocation = mdFileLocation.Replace(@"\", "/");
            }

            mdFileLocation = Path.Combine(_microserviceAssemblyLocation, mdFileLocation);
            return File.Exists(mdFileLocation) ? File.ReadAllText(mdFileLocation) : string.Empty;
        }

        #endregion
    }
}