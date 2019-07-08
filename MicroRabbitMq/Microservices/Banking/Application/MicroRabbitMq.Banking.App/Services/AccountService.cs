using MicroRabbitMq.Banking.App.Interfaces;
using MicroRabbitMq.Banking.App.Models;
using MicroRabbitMq.Banking.Domain.Commands;
using MicroRabbitMq.Banking.Domain.Interfaces;
using MicroRabbitMq.Banking.Domain.Models;
using MicroRabbitMq.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbitMq.Banking.App.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repository;
        private readonly IEventBus eventBus;

        public AccountService(IAccountRepository repository, IEventBus eventBus)
        {
            this.repository = repository;
            this.eventBus = eventBus;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return repository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(accountTransfer.FromAccount,accountTransfer.ToAccount,accountTransfer.TransferAmount);

            eventBus.SendCommand(createTransferCommand);
        }
    }
}
