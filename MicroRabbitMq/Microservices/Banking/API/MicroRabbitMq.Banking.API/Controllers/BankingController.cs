using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroRabbitMq.Banking.App.Interfaces;
using MicroRabbitMq.Banking.App.Models;
using MicroRabbitMq.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbitMq.Banking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService accountService;

        public BankingController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        // GET api/banking
        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return Ok(accountService.GetAccounts());
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountTransfer accountTransfer)
        {
            accountService.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }
    }
}
