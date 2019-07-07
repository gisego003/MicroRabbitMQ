using MicroRabbitMq.Domain.Core.Commands;
using MicroRabbitMq.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbitMq.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<TCommand>(TCommand command) where TCommand : Command;

        void Publish<TEvent>(TEvent @event) where TEvent : Event;

        void Subscribe<TEvent, THandler>()
            where TEvent : Event
            where THandler : IEventHandler<TEvent>;

    }
}
