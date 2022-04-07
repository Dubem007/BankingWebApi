using BankWebApi.Data.Models;
using BankWebApi.DTO;
using BankWebApi.Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountsController : Controller
    {
        private readonly IAccount _acct;

        public AccountsController(IAccount acct)
        {
           
            _acct = acct;
        }

        [HttpGet("GetAccountbyId")]
        public ActionResult GetAccountbyId(long Userid)
        {
            Accounts accts = new Accounts();
            accts = _acct.GetAccountById(Userid);
            return Created("", accts);
        }

        [HttpGet("GetAccountbyAcctnum")]
        public ActionResult GetAccountbyAcctnum(string acctnum)
        {
            Accounts accts = new Accounts();
            accts = _acct.GetAccountByAcctnum(acctnum);
            return Created("", accts);
        }


        //[Authorize]
        [HttpPost("CreateAccount")]
        public ActionResult CreateAccount([FromBody] Customers userdets)
        {
            var adddets = _acct.CreateAccount(userdets);
            if (adddets != null)
            {
                var accnum = adddets.AccountNumber;
                var acctfname = adddets.FirstName;
                var acctlname = adddets.LastName;
                var acctname = acctfname + acctlname;
                string note = $"Account has been created + {accnum} + for + {acctname}";
                return Created("", note);
            }
            return BadRequest();
        }

    }

}
