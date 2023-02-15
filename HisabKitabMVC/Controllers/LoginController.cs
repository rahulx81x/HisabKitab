using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
                    //Session["userName"] = name;
                    HttpContext.Session.SetString("userName",name);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
           
        }
    }
}
