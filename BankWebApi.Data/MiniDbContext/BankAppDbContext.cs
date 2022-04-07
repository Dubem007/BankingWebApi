using BankWebApi.Data.Models;
//using BankApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebApi.Data.MiniDbContext
{
    public class BankAppDbContext : DbContext
    {
        public BankAppDbContext(DbContextOptions<BankAppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Customers> Customers { get; set; }

        public virtual DbSet<Accounts> Account { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<TransferLogEntry> Transfers { get; set; }
    }
}
