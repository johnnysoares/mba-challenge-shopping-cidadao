using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using UTILCommon.Extensions;
using UTILCommon.Queeues.Core;
using UTILCommon.Queeues.Extensions;

namespace UTILCommon.Queeues.Consumers {

    public abstract class ConsumerQueue : IConsumerQueue {

        //Atributos

        //Dependencias
        protected readonly ILogger<IConsumerQueue> Logger;
        protected List<string> acceptedItens;
        protected List<string> returnedItens;

        /// <summary>
        /// Construtor
        /// </summary>
        protected ConsumerQueue(ILogger<IConsumerQueue> _Logger) {

            Logger = _Logger;
            
        }


        /// <summary>
        /// 
        /// </summary>
        public abstract Task receiveFromQueue();

        /// <summary>
        /// 
        /// </summary>
        public abstract bool isType(Type publisherType);
        
        /// <summary>
        /// 
        /// </summary>
        public virtual async Task receiveFromQueue(MqConfiguration MqConfiguration) {

            ConnectionFactory factory = MqConfiguration.toConnectionFactory();

            using (var connection = factory.CreateConnection()) {
                
                using (var channel = connection.CreateModel()) {

                    channel.QueueDeclare(MqConfiguration.queueName, true, false, false);

                    using (Subscription subscription = new Subscription(channel, MqConfiguration.queueName, false)) {

                        BasicDeliverEventArgs args;

                        if (!channel.IsOpen) {
                            this.Logger.LogError("The channel is no longer open, but we are still trying to process messages.");
                            return;
                        }

                        if (!connection.IsOpen) {
                            this.Logger.LogError("The connection is no longer open, but we are still trying to process message.");
                            return;
                        }

                        bool gotMessage = subscription.Next(500, out args);
                        if (!gotMessage) {
                            return;
                        }

                        var content = Encoding.UTF8.GetString(args.Body);

                        var RetornoQueue = JsonConvert.DeserializeObject<MessageQueueEnvio>(content);

                        var flagRecebido = await this.handleMessage(RetornoQueue.payload);

                        if (flagRecebido) {
                            subscription.Ack(args);
                        }
                    }

                    /*
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (ch, ea) => {

                        // received message  
                        

                        this.Logger.LogInformation($"consumer received {content}");

                        // handle the received message  
                        await handleMessage(content);

                        //
                        channel.BasicAck(ea.DeliveryTag, false);

                    };

                    consumer.Shutdown += OnConsumerShutdown;
                    consumer.Registered += OnConsumerRegistered;
                    consumer.Unregistered += OnConsumerUnregistered;
                    consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

                    channel.BasicConsume(MqConfiguration.queueName, true, consumer);
                    */
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual async Task<bool> handleMessage(string content) {

            bool flagResult =  await Task.Run(() => {

                this.Logger.LogInformation($"consumer received {content}");

                return true;
            });

            return flagResult;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) {

            this.Logger.LogInformation($"consumer OnConsumerConsumerCancelled");
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) {

            this.Logger.LogInformation($"consumer OnConsumerUnregistered");
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) {

            this.Logger.LogInformation($"consumer OnConsumerRegistered");
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) {

            this.Logger.LogInformation($"consumer OnConsumerShutdown");

        }

        /// <summary>
        /// 
        /// </summary>
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) {

            this.Logger.LogInformation($"consumer RabbitMQ_ConnectionShutdown");
        }
    }

}
