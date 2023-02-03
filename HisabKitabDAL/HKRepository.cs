using HisabKitabDAL.Models;
using System.Diagnostics.SymbolStore;

namespace HisabKitabDAL
{
    public class HKRepository
    {
        HisabKitabDbContext context;
        public HKRepository()
        {
            context = new HisabKitabDbContext(); 
        }

        public string GetBalance(int userId)
        {
            decimal balance = 0;
            try
            {
                    var tranList = (from tran in context.Transactions select tran).ToList();
                foreach (var tran in tranList)
                {
                    if (tran.Type == "C")
                    {
                        balance += tran.Amount;
                    }
                    else if (tran.Type == "D")
                    {
                        balance -= tran.Amount;
                    }
                }
                return balance.ToString();
            }
            catch
            {
                string errMessage = "Error";
                return errMessage  ;
            }
           
        }
        public List<Transaction>? GetAllTransaction(int UserId)
        {
            try
            {
                var tranList = (from tran in context.Transactions select tran).ToList();
                return tranList;
            }
            catch
            {
                return null;
            }
        }
        public List<Transaction>? GetCreditTransaction(int UserId)
        {
            try
            {
                var tranList = (from tran in context.Transactions where tran.Type=="C" select tran).ToList();
                return tranList;
            }
            catch
            {
                return null;
            }
        }
        public List<Transaction>? GetDebitTransaction(int UserId)
        {
            try
            {
                var tranList = (from tran in context.Transactions where tran.Type == "D" select tran).ToList();
                return tranList;
            }
            catch
            {
                return null;
            }
        }

        public bool AddUser(string username, string password)
        {
            User user = new User();
            user.UserName= username;
            user.UserPassword= password;
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Separate functions for adding credit and debit logs
        //public bool AddCredit(int userId,DateTime date,decimal amount)
        //{
        //    Transaction tran = new Transaction();
        //    tran.UserId= userId;
        //    tran.Date= date;
        //    tran.Amount= amount;
        //    tran.Type = "C";
        //    try
        //    {
        //        context.Transactions.Add(tran);
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}
        //public bool AddDebit(int userId, DateTime date, decimal amount)
        //{
        //    Transaction tran = new Transaction();
        //    tran.UserId = userId;
        //    tran.Date = date;
        //    tran.Amount = amount;
        //    tran.Type = "D";
        //    try
        //    {
        //        context.Transactions.Add(tran);
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    } 
        #endregion

        public bool AddTran(int userId, DateTime date, decimal amount, string type)
        {
            Transaction tran = new Transaction();
            tran.UserId = userId;
            tran.Date = date;
            tran.Amount = amount;
            tran.Type = type;
            try
            {
                context.Transactions.Add(tran);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool CheckUserPassword(string username,string password)
        {
            try
            {
                var userpwd = (from user in context.Users where user.UserName == username select user.UserPassword);
                if(userpwd != null)
                {
                    if (userpwd.ToString() == password)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
            
        }

    }
        
    
}