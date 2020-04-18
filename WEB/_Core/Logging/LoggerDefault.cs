using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;
using System.Diagnostics;
using ILogger = Serilog.ILogger;

namespace WEB._Core.Logging {

    public class LoggerDefault {

        /// <summary>
        /// 
        /// </summary>
        public static ILogger createLogger(IConfiguration configuration) {

            //var configVariables = configuration.GetSection("Graylog");

            //var urlEndpoint = configVariables.GetValue<string>("hostname");
            //int portNumber = configVariables.GetValue<int>("port");

            //GraylogSinkOptions configLogger = new GraylogSinkOptions();
            //configLogger.HostnameOrAddress = urlEndpoint;
            //configLogger.Port = portNumber;
            //configLogger.TransportType = TransportType.Http;

            ILogger logger = new LoggerConfiguration()
                                    .Enrich.FromLogContext()
                                    .WriteTo
                                    .File("local-log.txt")
                                    .CreateLogger();

            //Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            return logger;
        }
    }
}
