using MediatR;
using MicroRabbitMq.Banking.Domain.Commands;
using MicroRabbitMq.Banking.Domain.Events;
using MicroRabbitMq.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroRabbitMq.Banking.Domain.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus bus;

        public TransferCommandHandler(IEventBus bus)
        {
            this.bus = bus;
        }

        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            // Publish event to Rabbitmq

            bus.Publish(new TransferCreatedEvent(request.From, request.To, request.Amount));

            return Task.FromResult(true);
        }
    }
}
