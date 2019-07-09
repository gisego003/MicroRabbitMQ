using MediatR;
using MicroRabbitMq.Domain.Core.Bus;
using MicroRabbitMq.Domain.Core.Commands;
using MicroRabbitMq.Domain.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbitMq.Infraestructure.Bus
{
    public sealed class RabbitMqBus : IEventBus
    {
        private readonly IMediator mediator;
        private readonly Dictionary<string, List<Type>> handlers;
        private readonly List<Type> eventsTypes;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public RabbitMqBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            this.mediator = mediator;
            this.serviceScopeFactory = serviceScopeFactory;
            this.handlers = new Dictionary<string, List<Type>>();
            this.eventsTypes = new List<Type>();
        }

        public Task SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            var send = mediator.Send(command);
            return send;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var factory = new ConnectionFactory() { HostName = Configs.Host };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", eventName, null, body);

            }
        }

        public void Subscribe<TEvent, THandler>()
            where TEvent : Event
            where THandler : IEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);

            var eventName = eventType.Name;
            var handlerType = typeof(THandler);

            if (!eventsTypes.Contains(eventType))
                eventsTypes.Add(eventType);

            if (!handlers.ContainsKey(eventName))
                handlers.Add(eventName, new List<Type>());

            if (handlers[eventName].Any(x => x.GetType() == handlerType))
                throw new ArgumentException($"Handler type {handlerType.Name} already is registered for '{eventName}'");

            handlers[eventName].Add(handlerType);

            StartBasicConsumer<TEvent>();

        }

        private void StartBasicConsumer<TEvent>() where TEvent : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = Configs.Host,
                DispatchConsumersAsync = true,
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(TEvent).Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(eventName, true, consumer);

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body);

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (handlers.ContainsKey(eventName))
            {

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var subscriptions = handlers[eventName];

                    foreach (var subscription in subscriptions)
                    {
                        //var handler = Activator.CreateInstance(subscription);
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null) continue;

                        var eventType = eventsTypes.SingleOrDefault(x => x.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        var method = concreteType.GetMethod("Handle");
                        await (Task)method.Invoke(handler, new object[] { @event });

                    }
                }
            }
        }
    }
}
