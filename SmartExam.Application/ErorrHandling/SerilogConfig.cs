using Serilog;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.ErorrHandling
{
    public static class SerilogConfig
    {
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
