﻿using MicroRabbitMq.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbitMq.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
    }
}
