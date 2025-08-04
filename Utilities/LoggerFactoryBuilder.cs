using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace PlaywrightTests.Utilities
{
    public static class LoggerFactoryBuilder
    {
        private static ILoggerFactory _loggerFactory;

        public static ILoggerFactory GetLoggerFactory()
        {
            if (_loggerFactory == null)
            {
                // Configurar Serilog
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("Logs/test-log-.log",
                                  rollingInterval: RollingInterval.Day,
                                  retainedFileCountLimit: 7,
                                  outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                    .CreateLogger();

                _loggerFactory = new SerilogLoggerFactory(Log.Logger);
            }

            return _loggerFactory;
        }

        public static ILogger CreateLogger(Type type)
        {
            return  GetLoggerFactory().CreateLogger(type.FullName ?? type.Name);
        }

    }
}