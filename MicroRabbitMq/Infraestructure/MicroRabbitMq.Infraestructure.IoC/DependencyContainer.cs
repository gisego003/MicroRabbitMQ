using MediatR;
using MicroRabbitMq.Banking.App.Interfaces;
using MicroRabbitMq.Banking.App.Services;
using MicroRabbitMq.Banking.Data.Context;
using MicroRabbitMq.Banking.Data.Repository;
using MicroRabbitMq.Banking.Domain.CommandHandlers;
using MicroRabbitMq.Banking.Domain.Commands;
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

            //Domain banking commands
            services.AddTransient<IRequestHandler<CreateTransferCommand,bool>,TransferCommandHandler>();

            //Application services
            services.AddTransient<IAccountService, AccountService>();
            
            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
            
            #endregion

        }
    }
}
