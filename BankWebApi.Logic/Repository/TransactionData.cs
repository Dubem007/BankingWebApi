using BankWebApi.Data.MiniDbContext;
using BankWebApi.Data.Models;
using BankWebApi.Logic.Interface;
using BankWebApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace BankWebApi.Logic.Repository
{
   public class TransactionData: ITransaData
    {
        private readonly BankAppDbContext _db;
        private readonly ILoggerService _loggerService;
        public TransactionData(
            ILoggerService loggerService, BankAppDbContext db)
        {

            _loggerService = loggerService;
            _db = db;
        }
        public decimal GetMyBalance(long userID)
        {
            ReturnUser access = new ReturnUser();
            decimal myBalance = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT balance FROM account WHERE userid = @userid;", conn);
                    cmd.Parameters.AddWithValue("@userid", userID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        myBalance = Convert.ToDecimal(reader["balance"]);
                    }

                    return myBalance;
                }
                //string query = "SELECT balance FROM accounts WHERE user_id = @0";
                //var myacct = _db.Account.FindAsync(userID);
                //myBalance = myacct.Result.Balance;

                //return myBalance;

            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public decimal GetBalance(string accountnum)
        {
            ReturnUser access = new ReturnUser();
            decimal myBalance = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT balance FROM account WHERE accountnumber = @account;", conn);
                    cmd.Parameters.AddWithValue("@account", accountnum);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        myBalance = Convert.ToDecimal(reader["balance"]);
                    }

                    return myBalance;
                }
                
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public int GetMyUserID()
        {
            var user = /*User.Identity.Name*/ "Michael";
            int userID = 1;

            //foreach (var claim in User.Claims)
            //{
            //    if (claim.Type == "sub")
            //    {
            //        userID = int.Parse(claim.Value);
            //    }
            //}
            return userID;
        }

        public bool UpdateMyBalance(TransferData transferData, long userID)
        {
            //int UserID = Convert.ToInt32(userID);
            //decimal myBalance = GetMyBalance(UserID);
            decimal myBalance = GetBalance(transferData.From_AccountNumber);
            decimal newBalance = myBalance - transferData.TransferAmount;

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                  //"Data Source=ServerName;" +
                  //"Initial Catalog=DataBaseName;" +
                  //"Integrated Security=SSPI;";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE account SET balance = @newBalance WHERE accountnumber = @account;", conn);
                    cmd.Parameters.AddWithValue("@newBalance", newBalance);
                    cmd.Parameters.AddWithValue("@account", transferData.From_AccountNumber);

                    int count = cmd.ExecuteNonQuery();
                    
                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public bool UpdateBeneficiaryBalance(TransferData transferData)
        {
            decimal benBalance = GetBalance(transferData.To_AccountNumber);
            decimal newBalance = benBalance + transferData.TransferAmount;


            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                    //"Data Source=ServerName;" +
                    //"Initial Catalog=DataBaseName;" +
                    //"Integrated Security=SSPI;";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE account SET balance = @newBalance WHERE accountnumber = @account;", conn);
                    cmd.Parameters.AddWithValue("@newBalance", newBalance);
                    cmd.Parameters.AddWithValue("@account", transferData.To_AccountNumber);

                    int count = cmd.ExecuteNonQuery();
                    //var myacct = _db.Account.FindAsync(transferData.To_AccountNumber);
                    //benBalance = myacct.Result.Balance;
                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }
        public bool TopUpBalance(TransData transferData)
        {
            //int UserID = Convert.ToInt32(userID);
            decimal myBalance = GetBalance(transferData.AccountNumber);
            decimal newBalance = myBalance + transferData.TransferAmount;

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                    
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE account SET balance = @newBalance WHERE accountnumber = @account;", conn);
                    cmd.Parameters.AddWithValue("@newBalance", newBalance);
                    cmd.Parameters.AddWithValue("@account", transferData.AccountNumber);

                    int count = cmd.ExecuteNonQuery();
                    //var myacct = _db.Account.FindAsync(transferData.AccountNumber);
                    //myBalance = myacct.Result.Balance;
                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public bool AddTransfer(TransferLogEntry transferlog)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO transfers (transfertypeid, transferstatus, accountfrom, accountto, amount, narration) VALUES (@transfertypeid, @transferstatus, @accountfrom, @accountto, @amount,@narration);", conn);
                    cmd.Parameters.AddWithValue("@transfertypeid", transferlog.TransferTypeId);
                    cmd.Parameters.AddWithValue("@transferstatus", transferlog.TransferStatus);
                    cmd.Parameters.AddWithValue("@accountfrom", transferlog.AccountFrom);
                    cmd.Parameters.AddWithValue("@accountto", transferlog.AccountTo);
                    cmd.Parameters.AddWithValue("@amount", transferlog.Amount);
                    cmd.Parameters.AddWithValue("@narration", transferlog.Narration);

                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
