using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace UTILCommon.Jobs.Core {

    public class CustomJobFactory : IJobFactory {
        
        //Dependencies
        private readonly IServiceProvider ServiceProvider;

        /// <summary>
        /// Construtor
        /// </summary>
        public CustomJobFactory(IServiceProvider _ServiceProvider) {

            ServiceProvider = _ServiceProvider;
        }

        /// <summary>
        /// Override para que o Job possa ser injetado com suas dependências
        /// </summary>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) {
            
            return ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReturnJob(IJob job) {
        }
    }

}
