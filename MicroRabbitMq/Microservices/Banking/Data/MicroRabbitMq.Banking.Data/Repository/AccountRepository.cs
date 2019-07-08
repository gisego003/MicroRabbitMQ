using MicroRabbitMq.Banking.Data.Context;
using MicroRabbitMq.Banking.Domain.Interfaces;
using MicroRabbitMq.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbitMq.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext db;

        public AccountRepository(BankingDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return db.Accounts;
        }
    }
}
