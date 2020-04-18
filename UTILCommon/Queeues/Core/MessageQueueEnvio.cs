using System;

namespace UTILCommon.Queeues.Core {

    public class MessageQueueEnvio {
        
        public int transactionOutId { get; set; }
        
        public string idItem { get; set; }
        
        public string transactionCode { get; set; }

        public string payload { get; set; }
        
        public string exchangeName { get; set; }
        
        public bool isSentExchange { get; set; }
        
        public DateTime dtCreated { get; set; }
        
        public DateTime dtSentToExchange { get; set; }
        
        public DateTime dtToResendToExchange { get; set; }        
    }

}
