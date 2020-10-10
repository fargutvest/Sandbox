using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;

namespace NLogSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = UseConfigFile();
            Log(logger);
            logger = UseRuntimeConfig();
            Log(logger);


            Console.ReadKey();
        }

        static void Log(ILogger logger)
        {
            logger.Info("Main start");
            logger.Trace("Version: {0}", Environment.Version.ToString());
            logger.Trace("OS: {0}", Environment.OSVersion.ToString());
            logger.Trace("Command: {0}", Environment.CommandLine.ToString());
            logger.Info("Main end");

            logger.Trace("trace log message");

            Colors(logger);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            string test = "test";
            logger.Trace(test);
            logger.Debug(test);
            logger.Info(test);
            logger.Warn(test);
            logger.Error(test);
            logger.Fatal(test);
        }

        static void Colors(ILogger logger)
        {
            foreach (ConsoleOutputColor item in Enum.GetValues(typeof(ConsoleOutputColor)))
            {
                logger.Debug($"DEBUG: {item.ToString()}");
            }
        }

        static ILogger UseRuntimeConfig()
        {
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets
            var consoleTarget = new ColoredConsoleTarget("target1")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };

            var collection = new List<string>();

            foreach (ConsoleOutputColor item in Enum.GetValues(typeof(ConsoleOutputColor)))
            {
                var regex = Guid.NewGuid().ToString();
                collection.Add(regex);
                consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule()
                {
                    ForegroundColor = item,
                    Regex = $" {item.ToString()}"
                });
            }

            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("target2")
            {
                FileName = "${basedir}/file.txt",
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.AddTarget(fileTarget);


            // Step 3. Define rules
            config.AddRuleForOneLevel(LogLevel.Error, fileTarget); // only errors to file
            config.AddRuleForAllLevels(consoleTarget); // all to console



            // Step 4. Activate the configuration
            LogManager.Configuration = config;

            // Example usage
            Logger logger = LogManager.GetLogger("Example");

            return logger;
        } 

        static ILogger UseConfigFile()
        {
            return LogManager.GetCurrentClassLogger();
        }
    }
}
