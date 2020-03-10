using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;

namespace FindMyKids.ProximityMonitor.Queues
{
    public class AMQPConnectionFactory : ConnectionFactory
    {
        protected AMQPOptions amqpOptions;

        public AMQPConnectionFactory(
            ILogger<AMQPConnectionFactory> logger,
            IOptions<AMQPOptions> serviceOptions) : base()
        {
            this.amqpOptions = serviceOptions.Value;

            this.UserName = amqpOptions.Username;
            this.Password = amqpOptions.Password;
            this.VirtualHost = amqpOptions.VirtualHost;
            this.HostName = amqpOptions.HostName;
            //this.Uri = new Uri (amqpOptions.Uri);
            this.Port = AmqpTcpEndpoint.UseDefaultPort;

            logger.LogInformation($"AMQP Connection configured for URI : {amqpOptions.Uri}");
        }
    }
}