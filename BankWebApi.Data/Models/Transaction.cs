using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebApi.Data.Models
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        //[ForeignKey("Accounts")]
        public string AccountNumber { get; set; }

        [Column(TypeName = "decimal(38,2)")]
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsDeactivated { get; set; }
    }
}
