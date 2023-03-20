using HisabKitabDAL;
using HisabKitabDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using AutoMapper;

namespace HisabKitabMVC.Controllers
{
    public class UserController : Controller
    {
        HKRepository repository;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper)
        {
            repository = new HKRepository();
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            ViewBag.user = HttpContext.Session.GetString("userName");
            ViewBag.userId = repository.GetUserId(ViewBag.user);
            ViewBag.balance = repository.GetBalance(ViewBag.userId);
            return View();
        }
        public ActionResult AddTransaction()
        {
            List<SelectListItem> typeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "C", Text = "Credit" },
                new SelectListItem { Value = "D", Text = "Debit" }
            };
            var model = new Models.Transaction();
            model.TypeOptions = typeOptions.ToList();
            return View(model);
        }
        public ActionResult SaveTransaction(Models.Transaction tran)
        {
            try
            {
                string user = HttpContext.Session.GetString("userName");
                int userId = repository.GetUserId(user);
                bool res = repository.AddTran(userId, tran.Date, tran.Amount, tran.Type.ToString(), tran.Remarks);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("AddTransaction");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " in User Controllr,AddTran");
                return RedirectToAction("AddTransaction");
            }
        }
        public ActionResult ViewTransaction()
        {
            string user = HttpContext.Session.GetString("userName");
            int userId = repository.GetUserId(user);
            List<Transaction> lstTranDAL = repository.GetAllTransaction(userId);
            List<Models.Transaction> lstTranMVC = new List<Models.Transaction>();
            try
            {
                foreach (var item in lstTranDAL)
                {
                    lstTranMVC.Add(_mapper.Map<Models.Transaction>(item));
                }
                return View(lstTranMVC);
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
