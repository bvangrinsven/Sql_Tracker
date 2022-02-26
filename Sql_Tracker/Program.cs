using Autofac;
using CommandLine;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Sql_Tracker.Engine;
using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker
{
    internal class Program
    {
        class Options
        {
            [Option(Default = false, SetName = "initdb", HelpText = "Init Database")]
            public bool InitDB { get; set; }

            [Option(Default = false, SetName = "pullstats", HelpText = "Pull Database Stats")]
            public bool PullStats { get; set; }
        }

        static Options options { get; set; }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run);

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ProgramModule>();

            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                ILogger<Program> log = scope.Resolve<ILogger<Program>>();

                if (options.InitDB)
                {
                    log.LogInformation("Init DB");
                    var initdb = scope.Resolve<IInitDB>();
                    initdb.Execute();
                }

                if (options.PullStats)
                {
                    log.LogInformation("Pull Stats");
                    var pullStats = scope.Resolve<IPullStats>();
                    pullStats.Execute();
                }

            }

            Console.ReadKey();
        }
        private static void Run(Options opts)
        {
            options = opts;
        }

    }
}
