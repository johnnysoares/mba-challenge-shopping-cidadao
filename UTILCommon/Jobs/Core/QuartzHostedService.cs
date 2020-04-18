using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;

namespace UTILCommon.Jobs.Core {

    public class QuartzHostedService : IHostedService {

        //Dependencias
        private readonly ILogger<QuartzHostedService> Logger;
        private readonly ISchedulerFactory        schedulerFactory;
        private readonly IJobFactory              jobFactory;
        private readonly IEnumerable<JobSchedule> jobSchedules;
        public           IScheduler               Scheduler { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public QuartzHostedService(ILogger<QuartzHostedService> _Logger,
                                   ISchedulerFactory        _schedulerFactory,
                                   IJobFactory              _jobFactory,
                                   IEnumerable<JobSchedule> _jobSchedules) {

            Logger = _Logger;
            schedulerFactory = _schedulerFactory;
            jobSchedules = _jobSchedules;
            jobFactory = _jobFactory;
        }


        /// <summary>
        /// 
        /// </summary>
        public async Task StartAsync(CancellationToken cancellationToken) {

            Scheduler = await schedulerFactory.GetScheduler(cancellationToken);

            Scheduler.JobFactory = jobFactory;

            Logger.LogInformation($"Iniciando QuartzHostedService {jobSchedules.Count()}");
            
            foreach (var jobSchedule in jobSchedules) {
                
                var job = createJob(jobSchedule);
                
                var trigger = createTrigger(jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task StopAsync(CancellationToken cancellationToken) {
            
            Logger.LogInformation("Stoping QuartzHostedService");
            
            await Scheduler?.Shutdown(cancellationToken);
        }

        /// <summary>
        /// Criacao de Jobs
        /// </summary>
        private static IJobDetail createJob(JobSchedule schedule) {
            var jobType = schedule.JobType;

            return JobBuilder
               .Create(jobType)
               .WithIdentity(jobType.FullName)
               .WithDescription(jobType.Name)
               .Build();
        }

        /// <summary>
        /// Criacao de gatilho para tarefas
        /// </summary>
        private static ITrigger createTrigger(JobSchedule schedule) {

            return TriggerBuilder.Create()
                               .WithIdentity($"{schedule.JobType.FullName}.trigger")
                               .WithCronSchedule(schedule.cronExpression)
                               .WithDescription(schedule.cronExpression)
                               .Build();
        }
    }

}
