using BankWebApi.Data.Models;
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
    public class TransactionController : ControllerBase
    {
        private readonly ITransaData _transdata;
        public TransferLogEntry transferlog = new TransferLogEntry();

        public TransactionController(ITransaData transdata)
        {

            _transdata = transdata;
        }
        
        //[Authorize]
        [HttpPost("TopUpBalance")]
        public ActionResult UpdateBalance(TransData transData)
        {
            //int userID = _transdata.GetMyUserID();
            //long UserId = Convert.ToInt64(userID);
            decimal myBalance = _transdata.GetBalance(transData.AccountNumber);

            if (transData.TransferAmount != 0)
            {
                //Should we put a a try catch here?
                bool bal = _transdata.TopUpBalance(transData);
                //bool receiver = accountDAO.UpdateUserBalance(transferData);

                if (bal == true)
                {
                    string fel = $"Customer Balance Updated Successfully for + {transData.AccountNumber}";
                   return Created("", fel);
                    
                }
            }
            return BadRequest();
        }

        [HttpPost("FundTransfer")]
        public ActionResult FundTransfer(TransferData transferData)
        {
            int userID = _transdata.GetMyUserID();
            long UserId = Convert.ToInt64(userID);
            
            try 
            {
                if (transferData.TransferAmount != 0)
                {
                    bool mybal = _transdata.UpdateMyBalance(transferData, UserId);
                    bool receiver = _transdata.UpdateBeneficiaryBalance(transferData);

                    if (mybal == true && receiver == true)
                    {
                        bool transferAdded = AddTransfer(transferData);

                        if (transferAdded == true)
                        {
                            string fel = $"FundTransfer was successful from + {transferData.From_AccountNumber} + to + {transferData.To_AccountNumber}";
                            return Created("", fel);
                        }

                    }
                    else 
                    {
                        bool transferAdded = AddTransfer2(transferData);

                        if (transferAdded == true)
                        {
                            string fel = $"FundTransfer failed from + {transferData.From_AccountNumber} + to + {transferData.To_AccountNumber}";
                            return Created("", fel);
                        }
                    }
                    
                }
                 
            } 
            catch(Exception ex) 
            {
                return BadRequest();

            }


            return BadRequest();

        }

        private bool AddTransfer(TransferData transferData)
        {
            //int userID = GetMyUserID();
            int typeid = 2;
            transferlog.TransferTypeId = typeid;
            transferlog.TransferStatus = "SUCCESSFUL";
            transferlog.AccountFrom = transferData.From_AccountNumber;
            transferlog.AccountTo = transferData.To_AccountNumber;
            transferlog.Amount = transferData.TransferAmount;
            transferlog.Narration = transferData.Narration;

            bool transferLogAdded = _transdata.AddTransfer(transferlog);

            if (transferLogAdded == true)
            {
                return true;
            }
            return false;
        }

        private bool AddTransfer2(TransferData transferData)
        {
            //int userID = GetMyUserID();

            transferlog.TransferTypeId = 2;
            transferlog.TransferStatus = "FAILED";
            transferlog.AccountFrom = transferData.From_AccountNumber;
            transferlog.AccountTo = transferData.To_AccountNumber;
            transferlog.Amount = transferData.TransferAmount;

            bool transferLogAdded = _transdata.AddTransfer(transferlog);

            if (transferLogAdded == true)
            {
                return true;
            }
            return false;
        }
    }
}
