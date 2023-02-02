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

    }
}