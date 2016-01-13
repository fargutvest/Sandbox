using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogSample
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Main start");

            logger.Trace("Version: {0}", Environment.Version.ToString());
            logger.Trace("OS: {0}", Environment.OSVersion.ToString());
            logger.Trace("Command: {0}", Environment.CommandLine.ToString());

            logger.Info("Main end");

        }
    }
}
