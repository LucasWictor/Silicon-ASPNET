using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_MVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignIn()
        {
            ViewData["Title"] = "Sign In";
            return View();
        }

        public IActionResult SignUp()
        {
            ViewData["Title"] = "Sign Up";
            return View();
        }

        public IActionResult SignOut()
        {
            // logic here
            return RedirectToAction("Index", "Home");
        }
    }
}