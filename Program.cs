using System;
using System.IO;
using CommandLine;
using Newtonsoft.Json.Linq;

namespace WorkspaceManager
{
    static class Program
    {
        private class Options
        {
            private const string _configHelpText = "Provide a path to a .json configuration file.";

            [Option('c', "config", Required = true, HelpText = _configHelpText)]
            public string Config { get; set; }
        }

        private static void RunOptions(Options o)
        {
            // Make sure the config file exists before proceeding.
            if (!File.Exists(o.Config))
            {
                Console.WriteLine($"Config file does not exist: {o.Config}");
                return;
            }
            Console.WriteLine($"Using config from {o.Config}");
            string json = File.ReadAllText(o.Config);
            // Console.WriteLine("Parsing json:");
            // Console.WriteLine(json);
            string[] config = JArray.Parse(json).ToObject<string[]>();

            foreach (string app in config)
            {
                ApplicationControl.CreateAndRun(app);
            }
        }

        static void Main(string[] args)
        {
            // Parse arguments.
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => RunOptions(o));
        }
    }
}
