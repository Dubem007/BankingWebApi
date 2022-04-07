
using BankWebApi.Data.Models;
//using BankApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankWebApi.Logic.Interface
{
    public interface IAccount
    {
        //Accounts CreateAccount(Accounts wallet);
        //Task<Accounts> CreateAccount(Accounts wallet);
        Accounts CreateAccount(Customers custdets);
        Accounts GetAccountByAcctnum(string acctnum);
        Accounts GetAccountById(long userid);
    }
}
