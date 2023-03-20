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
                var tranList = (from tran in context.Transactions where tran.UserId==userId select tran ).ToList();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string errMessage = "Error";
                return errMessage;
            }

        }
        public List<Transaction>? GetAllTransaction(int userId)
        {
            try
            {
                var tranList = (from tran in context.Transactions where tran.UserId == userId select tran).ToList();
                return tranList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<Transaction>? GetCreditTransaction(int userId)
        {
            try
            {
                var tranList = (from tran in context.Transactions where (tran.Type == "C" && tran.UserId == userId) select tran).ToList();
                return tranList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<Transaction>? GetDebitTransaction(int userId)
        {
            try
            {
                var tranList = (from tran in context.Transactions where (tran.Type == "D" && tran.UserId == userId) select tran).ToList();
                return tranList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool AddUser(string username, string password)
        {
            User user = new User();
            user.UserName = username;
            user.UserPassword = password;
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #region Separate functions for adding credit and debit logs
        public bool AddCredit(int userId, DateTime date, decimal amount)
        {
            Transaction tran = new Transaction();
            tran.UserId = userId;
            tran.Date = date;
            tran.Amount = amount;
            tran.Type = "C";
            try
            {
                context.Transactions.Add(tran);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool AddDebit(int userId, DateTime date, decimal amount)
        {
            Transaction tran = new Transaction();
            tran.UserId = userId;
            tran.Date = date;
            tran.Amount = amount;
            tran.Type = "D";
            try
            {
                context.Transactions.Add(tran);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            #endregion
        }
        public bool AddTran(int userId, DateTime date, decimal amount, string type,string remark)
        {
            Transaction tran = new Transaction();
            tran.UserId = userId;
            tran.Date = date;
            tran.Amount = amount;
            tran.Type = type;
            tran.Remarks = remark;
            try
            {
                context.Transactions.Add(tran);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+"inside repo addtran");
                return false;
            }

        }

        public bool CheckUserPassword(string username, string password)
        {
            try
            {
                List<string> userpwd = (from user in context.Users where user.UserName == username select user.UserPassword).ToList();
                if (userpwd != null)   
                {
                    foreach(var pwd in userpwd) 
                    {
                        if (pwd == password)
                        {
                            return true;
                        }
                    }
                    //string pwd = userpwd.ToString();
                    
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public int GetUserId(string username)
        {
            try
            {
                List<int> userId =(from user in context.Users where user.UserName == username select user.UserId).ToList();
                if (userId != null)
                {
                    return userId[0];

                }
                return 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

    }


}