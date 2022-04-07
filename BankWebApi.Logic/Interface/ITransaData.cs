using BankWebApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankWebApi.Logic.Interface
{
    public interface ITransaData
    {
        decimal GetMyBalance(long userID);
        bool UpdateMyBalance(TransferData transferData, long userID);
        int GetMyUserID();
        decimal GetBalance(string accountnum);
        bool TopUpBalance(TransData transferData);
        bool UpdateBeneficiaryBalance(TransferData transferData);
        bool AddTransfer(TransferLogEntry transferlog);

    }
}
