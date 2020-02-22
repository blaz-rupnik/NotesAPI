using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerService
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}
