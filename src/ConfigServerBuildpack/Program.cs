using System;

namespace ConfigServerBuildpack
{
    public class Program
    {
        static int Main(string[] args)
        {
            return new ConfigServerBuildpack().Run(args);
        }
    }
}