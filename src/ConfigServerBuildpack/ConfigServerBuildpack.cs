using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Extensions.Configuration.Placeholder;

namespace ConfigServerBuildpack
{
    public class ConfigServerBuildpack : SupplyBuildpack 
    {

        protected override void Apply(string buildPath, string cachePath, string depsPath, int index)
        {
            var myDependenciesDirectory = Path.Combine(depsPath, index.ToString()); // store any runtime dependencies not belonging to the app in this directory
            
            Console.WriteLine($"===Applying {nameof(ConfigServerBuildpack)}===");
            
            EnvironmentalVariables["MY_SETTING"] = "value"; // set any runtime environmental variables
            
        }

        protected override void PreStartup(string buildPath, string depsPath, int index)
        {
            var root = new ConfigurationBuilder()
                .AddCloudFoundry()
                .AddEnvironmentVariables()
                .AddConfigServer()
                .Build();
            var configServerProvider = root.Providers.OfType<ConfigServerConfigurationProvider>().First();
            var appName = root.GetValue<string>("spring:application:name");
            var placeholderProvider = new PlaceholderResolverProvider(new []{configServerProvider});
            var configServerRoot = new ConfigurationRoot(new IConfigurationProvider[]{placeholderProvider});
            var seperator = Environment.GetEnvironmentVariable("CONFIG_SEPERATOR");
            Console.WriteLine($"=== Registering config server values for app {appName} as environmental variables ===");
            foreach (var item in configServerRoot.AsEnumerable())
            {
                var key = seperator == null ? item.Key.ToUpper() : item.Key.Replace(":", seperator).ToUpper();
                EnvironmentalVariables[key] = item.Value;
                Console.WriteLine($"> {key}");
            }
        }

    }
}
