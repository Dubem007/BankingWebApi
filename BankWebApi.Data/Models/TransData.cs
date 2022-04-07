using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankWebApi.Data.Models
{
   public class TransData
    {
        [DataType(DataType.Text)]
        public string AccountNumber { get; set; }
        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Amount is invalid.")]
        public decimal TransferAmount { get; set; }
    }
}
