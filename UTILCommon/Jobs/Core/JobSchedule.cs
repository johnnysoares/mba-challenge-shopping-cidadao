using System;

namespace UTILCommon.Jobs.Core {

    public class JobSchedule {

        /// <summary>
        /// Construtor 
        /// </summary>
        public JobSchedule(Type jobType, string _cronExpression) {
            
            JobType = jobType;
            
            cronExpression = _cronExpression;
            
        }

        public Type   JobType        { get; }
        
        public string cronExpression { get; }
    }

}
