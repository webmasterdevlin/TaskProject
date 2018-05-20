using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TaskApi.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            throw new NotImplementedException();
        }
    }
}
