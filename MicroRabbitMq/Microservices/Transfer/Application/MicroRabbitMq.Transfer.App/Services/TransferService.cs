using MicroRabbitMq.Domain.Core.Bus;
using MicroRabbitMq.Transfer.App.Interfaces;
using MicroRabbitMq.Transfer.Domain.Interfaces;
using MicroRabbitMq.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbitMq.Transfer.App.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository transferRepository;
        private readonly IEventBus bus;

        public TransferService(ITransferRepository transferRepository, IEventBus bus)
        {
            this.transferRepository = transferRepository;
            this.bus = bus;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return transferRepository.GetTransferLogs();
        }
    }
}
