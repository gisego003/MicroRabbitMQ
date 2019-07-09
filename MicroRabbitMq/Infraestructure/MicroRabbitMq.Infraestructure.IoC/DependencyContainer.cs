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
using MicroRabbitMq.Transfer.App.Interfaces;
using MicroRabbitMq.Transfer.App.Services;
using MicroRabbitMq.Transfer.Data.Context;
using MicroRabbitMq.Transfer.Data.Repository;
using MicroRabbitMq.Transfer.Domain.EventHandlers;
using MicroRabbitMq.Transfer.Domain.Events;
using MicroRabbitMq.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroRabbitMq.Infraestructure.IoC
{
    public class DependencyContainer
    {
        public static void Register(IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMqBus>(sp=>
            {
                var scopeFactory = sp.GetService<IServiceScopeFactory>();
                var mediator = sp.GetService<IMediator>();
                return new RabbitMqBus(mediator, scopeFactory);
            });

            //Handlers dependency Injection
            services.AddTransient<TransferEventHandler>();


            #region Banking Microservice

            //Domain banking commands
            services.AddTransient<IRequestHandler<CreateTransferCommand,bool>,TransferCommandHandler>();

            //Application services
            services.AddTransient<IAccountService, AccountService>();
            
            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();

            #endregion

            #region Transfer Microservice

            //Application services
            services.AddTransient<ITransferService, TransferService>();

            //Data
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<TransferDbContext>();
            
            //Domain Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            #endregion

        }
    }
}
