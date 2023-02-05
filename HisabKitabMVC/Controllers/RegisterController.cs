using Microsoft.AspNetCore.Mvc;
using HisabKitabDAL;

namespace HisabKitabMVC.Controllers
{
    public class RegisterController : Controller
    {
        HKRepository repository;
        public RegisterController()
        {
            repository = new HKRepository();
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult SaveRegister(Models.Users user)
        {
            try
            {
                bool result = repository.AddUser(user.UserName, user.UserPassword);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Register");
                }

            }
            catch
            {
                return RedirectToAction("Index", "Register");
            }


        }
    }
}
