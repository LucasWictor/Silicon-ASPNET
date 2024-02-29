using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_MVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Contact Us";
            return View();
        }
    }
}
