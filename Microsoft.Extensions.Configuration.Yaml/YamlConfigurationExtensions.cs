using Microsoft.Extensions.FileProviders;

using System;

namespace Microsoft.Extensions.Configuration.Yaml
{
    public static class YamlConfigurationExtensions
    {
        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional = false, bool reloadOnChange=false) =>
            AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
        //public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path) =>
        //    AddYamlFile(builder, provider: null, path: path, optional: false, reloadOnChange: false);

        //public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional) =>
        //    AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: false);

        //public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange) =>
        //    AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(Resources.FormatError_InvalidFilePath(), nameof(path));
            }

            return builder.AddYamlFile(s =>
            {
                s.FileProvider = provider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, Action<YamlConfigurationSource> configureSource) =>
            builder.Add(configureSource);
    }
}