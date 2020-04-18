using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using UTILCommon.Extensions;
using UTILCommon.Queeues.Core;
using UTILCommon.Queeues.Extensions;

namespace UTILCommon.Queeues.Publish {

    public abstract class PublisherQueue : IPublisherQueue {

        //Atributos

        //Dependencias
        protected readonly ILogger<PublisherQueue> Logger;
        protected List<string> acceptedItens;
        protected List<string> returnedItens;

        /// <summary>
        /// Construtor
        /// </summary>
        protected PublisherQueue(ILogger<PublisherQueue> _Logger) {

            Logger = _Logger;
            
            
        }


        /// <summary>
        /// 
        /// </summary>
        public abstract Task sendToQueue();

        /// <summary>
        /// 
        /// </summary>
        public abstract bool isType(Type publisherType);
        
        /// <summary>
        /// 
        /// </summary>
        public virtual void sendToQueue(MqConfiguration MqConfiguration, MessageQueueEnvio[] listTransactions) {

            this.acceptedItens = new List<string>();
            
            this.returnedItens = new List<string>();

            ConnectionFactory factory = MqConfiguration.toConnectionFactory();

            using (var connection = factory.CreateConnection()) {
                
                using (var channel = connection.CreateModel()) {
                    
                    channel.ConfirmSelect();

                    channel.WaitForConfirms();
            
                    /*
                    channel.BasicAcks += (sender, eventArgs) => {
                                     
                                             this.Logger.LogInformation("Queue Accepted {sender} - {eventArgs}", sender, eventArgs);
                                             
                                     
                                         };

                    channel.BasicNacks += (sender, eventArgs) => {
                                      
                                              this.Logger.LogInformation("Queue Rejected {sender} - {eventArgs}", sender, eventArgs);
                                      
                                          };
                    */
                    channel.BasicReturn += (sender, eventArgs) => {
                                      
                                               this.Logger.LogInformation("Queue Returned {sender} - {MessageId}", sender, eventArgs.BasicProperties.MessageId);
                                               
                                               this.returnedItens.Add( eventArgs.BasicProperties.MessageId);
                                      
                                           };
                    
                    foreach (var ItemQueue in listTransactions) {

                        sendItemQueue(MqConfiguration, ItemQueue, channel);
                    }
                    

                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void sendItemQueue(MqConfiguration MqConfiguration, MessageQueueEnvio ItemQueue, IModel channel) {

            string message = JsonConvert.SerializeObject(ItemQueue);

            var body = Encoding.UTF8.GetBytes(message);

            this.Logger.LogDebug("Body sended {message}", body);
            
            IBasicProperties props = channel.CreateBasicProperties();
            
            props.ContentType = "application/json";
            
            props.DeliveryMode = 2;

            props.Persistent = true;
            
            props.MessageId = ItemQueue.idItem;
            
            channel.BasicPublish(exchange: MqConfiguration.exchange,
                                 routingKey: MqConfiguration.routingKey,
                                 basicProperties: props,
                                 body: body, 
                                 mandatory:true);      
            
            this.acceptedItens.Add( ItemQueue.idItem );
        }
    }

}
