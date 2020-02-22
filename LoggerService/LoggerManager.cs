using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
