﻿using MicroRabbitMq.Domain.Core.Bus;
using MicroRabbitMq.Transfer.Domain.Events;
using MicroRabbitMq.Transfer.Domain.Interfaces;
using MicroRabbitMq.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbitMq.Transfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository transferRepository;
        
        public TransferEventHandler(ITransferRepository transferRepository)
        {
            this.transferRepository = transferRepository;
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            transferRepository.Add(new TransferLog()
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                TransferAmount = @event.Amount,
            });
            return Task.CompletedTask;
        }
    }
}
