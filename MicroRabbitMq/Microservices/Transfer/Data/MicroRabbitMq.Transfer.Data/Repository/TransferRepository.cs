using MicroRabbitMq.Transfer.Data.Context;
using MicroRabbitMq.Transfer.Domain.Interfaces;
using MicroRabbitMq.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbitMq.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferDbContext db;

        public TransferRepository(TransferDbContext db)
        {
            this.db = db;
        }

        public TransferLog Add(TransferLog transferLog)
        {
            var res = db.TransferLogs.Add(transferLog);
            db.SaveChanges();
            return res.Entity;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return db.TransferLogs;
        }
    }
}
