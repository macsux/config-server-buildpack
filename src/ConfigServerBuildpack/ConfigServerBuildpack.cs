using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            Console.WriteLine($"=== Applying {nameof(ConfigServerBuildpack)} ===");
            
        }

        protected override void PreStartup(string buildPath, string depsPath, int index)
        {
            Console.WriteLine($"=== {nameof(ConfigServerBuildpack)} PreStartup ===");
            var config = new AppConfig();
            var appName = config.Configuration.GetValue<string>("spring:application:name");
            Console.WriteLine($"=== Registering config server values for app {appName} as environmental variables ===");
            
            foreach (var item in config.Configuration.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Value)))
            {
                var key = item.Key.Replace(":", "_").ToUpper();
                EnvironmentalVariables[key] = item.Value;
                Console.WriteLine($"> {key}");
            }
            Console.WriteLine("---> App config is also available as JSON in CONFIG_SERVER_APP_JSON env var");
            EnvironmentalVariables["CONFIG_SERVER_APP_JSON"] = config.GetConfigJson();
        }
       
    }
}
