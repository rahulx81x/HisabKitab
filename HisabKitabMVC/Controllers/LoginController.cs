using Microsoft.AspNetCore.Mvc;
using HisabKitabDAL;

namespace HisabKitabMVC.Controllers
{
    public class LoginController : Controller
    {
        HKRepository repository;
        public LoginController()
        {
            repository = new HKRepository();
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult CheckLogin(string name,string pwd)
        {
            try 
            {
                bool result = repository.CheckUserPassword(name, pwd);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }
    }
}
