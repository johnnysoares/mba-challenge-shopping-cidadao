using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UTILCommon.Models;
using UTILCommon.Queeues.Core;

namespace UTILCommon.Queeues.Publish {

    public interface IPublisherQueue {
        
        /// <summary>
        /// 
        /// </summary>
        Task sendToQueue();

        /// <summary>
        /// 
        /// </summary>
        bool isType(Type publisherType);
        
        /// <summary>
        /// 
        /// </summary>
        void sendToQueue(MqConfiguration MqConfiguration, MessageQueueEnvio[] listTransactions);
    }

}