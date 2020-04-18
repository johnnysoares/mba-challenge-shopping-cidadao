using BLL.Atendimentos.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using UTILCommon.Jobs.Core;

namespace WEB._Core.DI {
    
    public class StartupDIJobs {

        /// <summary>
        /// 
        /// </summary>
        public static void register(IServiceCollection services) {

            //Core Quartz
            services.AddSingleton<IJobFactory, CustomJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            //Iniciar Hosted Services
            services.AddHostedService<QuartzHostedService>();

            //Job Publisher Solicitacao to Queue
            //services.AddSingleton<JobBuscadorSenhaRJ>();
            //services.AddSingleton(new JobSchedule(jobType: typeof(JobBuscadorSenhaRJ), _cronExpression: "0 */1 * ? * *")); // run every 5 minutes

            //services.AddSingleton<JobBuscadorSenhaMG>();
            //services.AddSingleton(new JobSchedule(jobType: typeof(JobBuscadorSenhaMG), _cronExpression: "0 */1 * ? * *")); // run every 5 minutes

            //services.AddSingleton<JobBuscadorSenhaCE>();
            //services.AddSingleton(new JobSchedule(jobType: typeof(JobBuscadorSenhaCE), _cronExpression: "0/15 * * * * ? *")); // run every 30s

            services.AddSingleton<JobBuscadorDadosCE>();
            services.AddSingleton(new JobSchedule(jobType: typeof(JobBuscadorDadosCE), _cronExpression: "0/4 * * * * ? *")); // run every 5s

        }

    }
}