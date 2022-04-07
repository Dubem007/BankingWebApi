using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebApi.Data.Models
{
    public class TransferLogEntry
    {
        [Key]
        public int TransferId { get; set; }
        public int TransferTypeId { get; set; }
        public string TransferStatus { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        [Column(TypeName = "decimal(38,2)")]
        public decimal Amount { get; set; }
        public string UserNameTo { get; set; }
        public string UserNameFrom { get; set; }
        public string Narration { get; set; }
    }
}
