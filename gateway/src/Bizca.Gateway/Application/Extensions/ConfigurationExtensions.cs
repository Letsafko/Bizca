namespace Bizca.Gateway.Application.Extensions
{
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using MMLib.SwaggerForOcelot.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class ConfigurationExtensions
    {
        private const string primaryConfigFile = "ocelot.json";

        private const string globalConfigFile = "ocelot.global.json";

        private const string subConfigPattern = @"^ocelot\.(.*?)\.json$";

        private static readonly string[] excludeConfigNames = { "ocelot.Development.json", "ocelot.starterkit.json" };

        public static IConfigurationBuilder MergeOcelotFiles(this IConfigurationBuilder builder)
        {
            string folder = ".";

            var reg = new Regex(subConfigPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            List<FileInfo> files = new DirectoryInfo(folder)
                .EnumerateFiles()
                .Where(fi => reg.IsMatch(fi.Name)
                             && !excludeConfigNames.Contains(fi.Name)
                             && !fi.Name.Contains("ignored"))
                .ToList();

            OcelotExtendedFileConfiguration fileConfiguration = BuildConfiguration(files);

            string json = JsonConvert.SerializeObject(fileConfiguration);

            File.WriteAllText(primaryConfigFile, json);

            builder.AddJsonFile(primaryConfigFile, false, false);

            return builder;
        }

        private static OcelotExtendedFileConfiguration BuildConfiguration(List<FileInfo> files)
        {
            var fileConfiguration = new OcelotExtendedFileConfiguration();

            foreach (FileInfo file in files)
            {
                if (files.Count > 1 && file.Name.Equals(primaryConfigFile, StringComparison.OrdinalIgnoreCase))
                    continue;

                BuildConfiguration(fileConfiguration, file);
            }

            return fileConfiguration;
        }

        private static void BuildConfiguration(OcelotExtendedFileConfiguration fileConfiguration,
            FileInfo file)
        {
            var ignoredRoutes = new OcelotExtendedFileConfiguration();

            string lines = File.ReadAllText(file.FullName);

            var config = JsonConvert.DeserializeObject<OcelotExtendedFileConfiguration>(lines);

            if (file.Name.Equals(globalConfigFile, StringComparison.OrdinalIgnoreCase))
                fileConfiguration.GlobalConfiguration = config.GlobalConfiguration;

            fileConfiguration.Aggregates.AddRange(config.Aggregates);

            foreach (ExtendedFileRoute route in config.Routes)
            {
                if (!route.Validate())
                {
                    ignoredRoutes.Routes.Add(route);
                    continue;
                }

                fileConfiguration.Routes.Add(route);
            }

            File.WriteAllText(string.Concat(Path.GetFileNameWithoutExtension(file.FullName), ".ignored.json"),
                JsonConvert.SerializeObject(ignoredRoutes));

            foreach (SwaggerEndPointOptions swaggerEndpoint in config.SwaggerEndPoints)
            {
                if (fileConfiguration.SwaggerEndPoints.Any(i => i.Key == swaggerEndpoint.Key)) continue;

                fileConfiguration.SwaggerEndPoints.Add(swaggerEndpoint);
            }
        }
    }
}