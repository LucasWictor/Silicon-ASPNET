using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Profile";
            return View();
        }
    }
}
