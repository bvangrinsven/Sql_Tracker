using Autofac;
using Microsoft.Extensions.Logging;
using Sql_Tracker.Engine.Interfaces;
using Sql_Tracker.Engine.Process;
using Sql_Tracker.Engine.Utilz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotenv.net;

namespace Sql_Tracker.Engine
{
    public class ProgramModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            DotEnv.Fluent()
                .WithExceptions()
                .WithTrimValues()
                .WithOverwriteExistingVars()
                .Load();

            builder.Register(handler => LoggerFactory.Create(ConfigureLogging))
                .As<ILoggerFactory>()
                .SingleInstance()
                .AutoActivate();

            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();

            builder.RegisterType<InitDB>().As<IInitDB>();
            builder.RegisterType<PullStats>().As<IPullStats>();

            builder.RegisterType<Config>().As<IConfig>().SingleInstance();

        }

        private static void ConfigureLogging(ILoggingBuilder log)
        {
            log.ClearProviders();
            log.SetMinimumLevel(LogLevel.Information);
            log.AddConsole();
        }
    }
}
