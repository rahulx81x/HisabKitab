using HisabKitabDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HisabKitabMVC.Controllers
{
    public class UserController : Controller
    {
        HKRepository repository;
        public UserController()
        {
            repository = new HKRepository(); 
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
                bool res = repository.AddTran(userId, tran.Date, tran.Amount, tran.Type.ToString(), tran.Remarks=" ");
                if (res)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("AddTransaction");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+" in User Controllr,AddTran");
                return RedirectToAction("AddTransaction");
            }
        }
    }
}
