using System;
using System.Linq;

namespace Lifecycle.Supply
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsWithCommand = new[] {"detect"}.Union(args).ToArray();
            new ConfigServerBuildpack.ConfigServerBuildpack().Run(argsWithCommand);
        }
    }
}