using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroRabbitMq.Transfer.App.Interfaces;
using MicroRabbitMq.Transfer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbitMq.Transfer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService transferService;

        public TransferController(ITransferService transferService)
        {
            this.transferService = transferService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TransferLog>> Get()
        {
            return Ok(transferService.GetTransferLogs());
        }
    }
}
