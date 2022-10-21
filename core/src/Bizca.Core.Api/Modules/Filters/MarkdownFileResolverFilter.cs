namespace Bizca.Core.Api.Modules.Filters
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public sealed class MarkdownFileResolverFilter : IDocumentFilter
    {
        private const string SwaggerIntroductionMarkdownFilePath = "Assets/Introduction.md";

        private readonly string _microserviceAssemblyLocation =
            Directory.GetParent(Assembly.GetEntryAssembly()!.Location).FullName;
        

        #region Methods

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!Directory.Exists(_microserviceAssemblyLocation)) return;

            GenerateIntroduction(swaggerDoc);
            foreach (KeyValuePair<string, OpenApiPathItem> path in swaggerDoc.Paths)
            {
                foreach (KeyValuePair<OperationType, OpenApiOperation> operation in path.Value.Operations)
                {
                    GenerateDescription(operation.Value);
                }
            }
        }
        
        private void GenerateIntroduction(OpenApiDocument swaggerDoc)
        {
            string markdown = ReadMarkdownFile(SwaggerIntroductionMarkdownFilePath);
            if (!string.IsNullOrEmpty(markdown)) swaggerDoc.Info.Description = markdown;
        }

        /// <summary>
        ///     Detects if the description field of an operation targets a markdown file (.MD file) and try set the file content as
        ///     the markdown description of the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <example>
        ///     In an endpoint, defines the "///
        ///     <remarks></remarks>
        ///     " header and put a relative path
        ///     to the .MD file. Example : "///
        ///     <remarks>/Assets/MyFile.md</remarks>
        ///     "
        /// </example>
        private void GenerateDescription(OpenApiOperation operation)
        {
            if (operation is null) 
                return;

            var markDownFileContent = ReadMarkdownFile(operation.Description);
            if (!string.IsNullOrWhiteSpace(markDownFileContent))
                operation.Description = markDownFileContent;
        }

        private string ReadMarkdownFile(string relativeFilePath)
        {
            if (string.IsNullOrWhiteSpace(relativeFilePath) || !relativeFilePath.EndsWith(".md", StringComparison.OrdinalIgnoreCase)) 
                return string.Empty;

            var mdFileLocation = relativeFilePath.TrimStart('/').TrimStart('\\');
            mdFileLocation = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? mdFileLocation.Replace("/", @"\") 
                : mdFileLocation.Replace(@"\", "/");

            mdFileLocation = Path.Combine(_microserviceAssemblyLocation, mdFileLocation);
            return File.Exists(mdFileLocation) ? File.ReadAllText(mdFileLocation) : string.Empty;
        }

        #endregion
    }
}