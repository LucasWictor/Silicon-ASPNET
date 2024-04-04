using AspNetCore_MVC.ViewModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using StatusCodes = Infrastructure.Models.StatusCodes;

namespace AspNetCore_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly ILogger<AuthController> _logger; 
        
        public AuthController(UserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/signup")]
        public IActionResult SignUp()
        {
           // _logger.LogInformation("Visited SignUp page");
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            //_logger.LogInformation("Attempt to sign up with email: {Email}", viewModel.Form.Email);
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUserAsync(viewModel.Form);
                if (result.StatusCode == Infrastructure.Models.StatusCodes.Ok)
                {
                    //_logger.LogInformation("SignUp successful for email: {Email}", viewModel.Form.Email);
                    return RedirectToAction("SignIn", "Auth");
                }
                else
                {
                    //_logger.LogWarning("SignUp failed for email: {Email}. Status code: {StatusCode}", viewModel.Form.Email, result.StatusCode);
                }
            }
            else
            {
               // _logger.LogWarning("SignUp attempt with invalid model state for email: {Email}", viewModel.Form.Email);
            }

            return View(viewModel);
        }

        //sign out
        public IActionResult SignOut()
        {
            //_logger.LogInformation("User signed out");
            return RedirectToAction("Index", "Home");
        }

        //sign in
        [HttpGet]
        [Route("/signin")]
        public IActionResult SignIn()
        {
            //_logger.LogInformation("Visited SignIn page");
            var viewModel = new SignInViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Route("/signin")]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SignInUserAsync(viewModel.Form);
                if (result.StatusCode == Infrastructure.Models.StatusCodes.Ok)
                {
                    return RedirectToAction("Details", "Account");
                }
                else
                {
                    viewModel.ErrorMessage = "Incorrect email or password";
                }
            }
            return View(viewModel);
        }
    }
}
