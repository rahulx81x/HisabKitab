using AutoMapper;
using HisabKitabDAL.Models;
using System.Runtime;

namespace HisabKitabMVC.Repository
{
    public class HKMapper:Profile
    {
        public HKMapper()
        {
            CreateMap<Transaction, Models.Transaction>();
            CreateMap<Models.Transaction, Transaction>();
        }
    }
}
