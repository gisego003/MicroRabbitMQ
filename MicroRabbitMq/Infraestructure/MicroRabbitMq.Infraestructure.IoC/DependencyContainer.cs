using MicroRabbitMq.Banking.App.Interfaces;
using MicroRabbitMq.Banking.App.Services;
using MicroRabbitMq.Banking.Data.Context;
using MicroRabbitMq.Banking.Data.Repository;
using MicroRabbitMq.Banking.Domain.Interfaces;
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
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMqBus>();

            #region Bank Microservice

            //Application services
            services.AddTransient<IAccountService, AccountService>();
            
            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
            
            #endregion

        }
    }
}
