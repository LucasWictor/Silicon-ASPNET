using AspNetCore_MVC.ViewModels;
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

        [Route("/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            return RedirectToAction("SignIn", "Auth");
        }

        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}