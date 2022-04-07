using AutoMapper;
using BankWebApi.Data.MiniDbContext;
using BankWebApi.Data.Models;
using BankWebApi.Logic.Interface;
//using BankApi.MiniDbContext;
//using BankApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BankWebApi.Logic.Repository
{
    public class AccountsRepo : IAccount
    {
        //private readonly UserManager<Customers> _userManager;
        //private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        private readonly BankAppDbContext _db;

        public AccountsRepo(

            //UserManager<Customers> userManager,
            //IMapper mapper,
            ILoggerService loggerService, BankAppDbContext db)
        {


            //_userManager = userManager;
            //_mapper = mapper;
            _loggerService = loggerService;
            _db = db;
        }
        public Accounts CreateAccount(Customers custdets)
        {
            var num = GenerateNumber();
            //var user = _mapper.Map<Customers>(custdets);
            //var createdUserResult = await _userManager.CreateAsync(user, custdets.PasswordHash);
            Accounts reqs = new Accounts();
            reqs = new Accounts()
            {
                FirstName = custdets.FirstName,
                LastName = custdets.LastName,
                Username = custdets.Username,
                Email = custdets.Email,
                Image = custdets.Image,
                PhoneNo = custdets.PhoneNo,
                UserId = custdets.Id,
                AccountNumber = num,
                Balance = 0,
                CreatedAt = DateTime.Now,

            };
            bool dets = SaveNewAccountDetails(reqs);

            if (dets == true)
            {
                return reqs;
            }
            else
            {
                return new Accounts();
            }
            //return reqs;
        }

        public Task<Accounts> CreateAccount(Accounts wallet)
        {
            throw new NotImplementedException();
        }

        private string GenerateNumber()
        {
            Random rng = new Random();
            int number = rng.Next(1, 1000000000);
            string digits = number.ToString("0000000000");
            return digits;
        }

        public bool SaveNewAccountDetails(Accounts reqs)
        {
            //if (reqs !=null) 
            //{
            //_db.Account.Add(reqs);
            //return true;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Account (AccountNumber, FirstName, LastName, UserName, Email, PhoneNo,Image,Balance,CreatedAt, UpdatedAt, IsDeleted, IsDeactivated) VALUES (@AccountNumber, @FirstName, @LastName, @UserName, @Email, @PhoneNo,@Image, @Balance,@CreatedAt,@UpdatedAt, @IsDeleted, @IsDeactivated);", conn);
                cmd.Parameters.AddWithValue("@AccountNumber", reqs.AccountNumber);
                cmd.Parameters.AddWithValue("@FirstName", reqs.FirstName);
                cmd.Parameters.AddWithValue("@LastName", reqs.LastName);
                cmd.Parameters.AddWithValue("@UserName", reqs.Username);
                cmd.Parameters.AddWithValue("@Email", reqs.Email);
                cmd.Parameters.AddWithValue("@PhoneNo", reqs.PhoneNo);
                cmd.Parameters.AddWithValue("@Image", reqs.Image);
                cmd.Parameters.AddWithValue("@Balance", reqs.Balance);
                cmd.Parameters.AddWithValue("@CreatedAt", Convert.ToDateTime(reqs.CreatedAt.ToString("dd/MM/yy")));
                cmd.Parameters.AddWithValue("@UpdatedAt", Convert.ToDateTime(reqs.UpdatedAt.ToString("dd/MM/yy")));
                cmd.Parameters.AddWithValue("@IsDeleted", reqs.IsDeleted);
                cmd.Parameters.AddWithValue("@IsDeactivated", reqs.IsDeactivated);
                //SqlDataReader reader = cmd.ExecuteReader();

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

        public Accounts GetAccountById(long userid)
        {
            Accounts accts = new Accounts();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Account WHERE userid = @userid", conn);
                cmd.Parameters.AddWithValue("@userid", userid);
               
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    accts = GetAccountFromReader(reader);
                }
                return accts;
            }
        }


        public Accounts GetAccountByAcctnum(string acctnum)
        {
            Accounts accts = new Accounts();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankingApp;Integrated Security=True;";
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Account WHERE accountnumber = @accountnumber", conn);
                cmd.Parameters.AddWithValue("@accountnumber", acctnum);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    accts = GetAccountFromReader(reader);
                }
                return accts;
            }
        }
        private Accounts GetAccountFromReader(SqlDataReader reader)
        {
            Accounts u = new Accounts()
            {
                UserId = Convert.ToInt64(reader["userid"]),
                Username = Convert.ToString(reader["username"]),
                AccountNumber = Convert.ToString(reader["accountnumber"]),
                FirstName = Convert.ToString(reader["firstname"]),
                LastName = Convert.ToString(reader["lastname"]),
                Email = Convert.ToString(reader["email"]),
                PhoneNo = Convert.ToString(reader["phoneno"]),
                Image = Convert.ToString(reader["image"]),
                Balance = Convert.ToDecimal(reader["balance"]),
                CreatedAt = Convert.ToDateTime(reader["createdat"]),
                UpdatedAt = Convert.ToDateTime(reader["updatedat"]),
                IsDeleted = Convert.ToBoolean(reader["isdeleted"]),
                IsDeactivated = Convert.ToBoolean(reader["isdeactivated"]),

            };

            return u;
        }
    }


}

