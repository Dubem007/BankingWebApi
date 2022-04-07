using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankWebApi.Data.Models
{
    public class TransferData
    {
        [DataType(DataType.Text)]
        public string From_AccountNumber { get; set; }
        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Amount is invalid.")]
        public decimal TransferAmount { get; set; }
        [DataType(DataType.Text)]
         public string To_AccountNumber { get; set; }
        [DataType(DataType.Text)]
         public string Narration { get; set; }
    }
}
