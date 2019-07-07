using MicroRabbitMq.Domain.Core.Bus;
using MicroRabbitMq.Infraestructure.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroRabbitMq.Infraestructure.IoC
{
    public class DependencyContainer
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMqBus>();
        }
    }
}
