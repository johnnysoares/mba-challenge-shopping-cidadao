namespace UTILCommon.Queeues.Core {

    public class MqConfiguration {
        
        public string hostname { get; set; }
        
        public int port { get; set; }
        
        public string username { get; set; }
        
        public string password { get; set; }
        
        public string vhost { get; set; }
        
        public string exchange { get; set; }
        
        
        public string exchangeType { get; set; }
        
        public string queueName { get; set; }

        public string routingKey { get; set; }

        
        public bool flagDurable { get; set; }
    }

}

