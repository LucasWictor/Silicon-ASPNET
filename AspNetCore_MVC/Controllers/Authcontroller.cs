using AspNetCore_MVC.ViewModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using StatusCodes = Infrastructure.Models.StatusCodes;

namespace AspNetCore_MVC.Controllers
{
    public class AuthController(UserService userService) : Controller
    {
        private readonly UserService _userService = userService;

        //sign up

        [HttpGet]
        [Route("/signup")]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUserAsync(viewModel.Form);
                if (result.StatusCode == StatusCodes.Ok)
                    return RedirectToAction("SignIn", "Auth");
            }
                return View(viewModel);
        }

        //sign out
        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }

        //sign in
        [HttpGet]
        [Route("/signin")]
        public IActionResult SignIn()
        {
            var viewModel = new SignInViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [Route("/signin")]
        public IActionResult SignIn(SignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
          
            //var result = _authService.SignIn(viewModel.Form);
            //if (result)
            //    return RedirectToAction("Account", "Details");

            viewModel.ErrorMessage = "Incorrect email or password";
            return View(viewModel);
            
        }
    }
}