using MicroRabbitMq.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbitMq.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            this.Timestamp = DateTime.Now;
        }
    }
}
