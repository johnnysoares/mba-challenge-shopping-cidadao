using BLL._Core.DI;
using DAL._Core.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WEB._Core.DI {
    public class StartupDI {

        /// <summary>
        /// 
        /// </summary>
        public static void register(IServiceCollection services, IConfiguration configuration) {

            loggingRegister(services, configuration);

            StartupDIWeb.register(services);
            
            StartupDIDAL.register(services);

            StartupDIBLL.register(services);
            
            StartupDIJobs.register(services);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void loggingRegister(IServiceCollection services, IConfiguration configuration) {

            services.AddLogging(loggingBuilder => {

                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));

                loggingBuilder.AddConsole();

                loggingBuilder.AddSerilog();

                loggingBuilder.AddDebug();
            });
        }
    }
}
