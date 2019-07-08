using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbitMq.Banking.App.Models
{
    public class AccountTransfer
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
