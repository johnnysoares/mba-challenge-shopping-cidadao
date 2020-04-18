using RabbitMQ.Client;
using UTILCommon.Queeues.Core;

namespace UTILCommon.Queeues.Extensions {

    public static class MqConfigurationExtensions {

        /// <summary>
        /// 
        /// </summary>
        public static ConnectionFactory toConnectionFactory(this MqConfiguration MqConfiguration) {
            
            var factory = new ConnectionFactory();
            
            factory.HostName = MqConfiguration.hostname;
            
            factory.Port = MqConfiguration.port;
            
            factory.UserName = MqConfiguration.username;
            
            factory.Password = MqConfiguration.password;
            
            factory.VirtualHost = MqConfiguration.vhost;

            return factory;
        }
    }

}
