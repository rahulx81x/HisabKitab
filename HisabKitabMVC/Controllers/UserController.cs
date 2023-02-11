using Microsoft.AspNetCore.Mvc;

namespace HisabKitabMVC.Controllers
{
    public class UserController : Controller
    {
        

        public IActionResult Index()
        {
            ViewBag.user = HttpContext.Session.GetString("userName");
            return View();
        }
    }
}
