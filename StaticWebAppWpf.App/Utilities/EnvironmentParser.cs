using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.Utilities
{
    public static class EnvironmentParser
    {
        public static string GetEnvironment(string[] args)
        {
            if (args.Length != 0 &&
                args.Contains("-dev"))
                return Environments.Development;

            return Environments.Production;
        }
    }
}
