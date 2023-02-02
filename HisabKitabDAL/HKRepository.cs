using HisabKitabDAL.Models;


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
    }
}