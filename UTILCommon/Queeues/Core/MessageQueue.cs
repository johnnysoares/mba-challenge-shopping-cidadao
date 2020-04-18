using System;

namespace UTILCommon.Queeues.Core {

    public class MessageQueue {
        
        public Guid messageGuid { get; set; }
        
        public string transactionCode { get; set; }
        
        public int transactionQueueId { get; set; }
        
        public string payload            { get; set; }
        
        public string exchangeName       { get; set; }
        
        public string queueName          { get; set; }
    }

}
