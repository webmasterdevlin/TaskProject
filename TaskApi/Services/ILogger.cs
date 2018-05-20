using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TaskApi.Services
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
//        void Log(Exception ex) => Log(LogLevel.Error, ex.ToString());
    }
}
