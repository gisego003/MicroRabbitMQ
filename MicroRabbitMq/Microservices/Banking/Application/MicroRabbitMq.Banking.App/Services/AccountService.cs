using MicroRabbitMq.Banking.App.Interfaces;
using MicroRabbitMq.Banking.Domain.Interfaces;
using MicroRabbitMq.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbitMq.Banking.App.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.repository = accountRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return repository.GetAccounts();
        }
    }
}
