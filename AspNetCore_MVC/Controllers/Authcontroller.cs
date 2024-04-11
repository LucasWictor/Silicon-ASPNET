using System.Security.Claims;
using AspNetCore_MVC.ViewModels;
using Infrastructure.Contexts;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StatusCodes = Infrastructure.Models.StatusCodes;

namespace AspNetCore_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly DataContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(DataContext context, UserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _context = context;
            _logger = logger;
        }

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
                {
                    return RedirectToAction("SignIn", "Auth");
                }
                else
                {
                    _logger.LogWarning("SignUp failed for email: {Email}. Status code: {StatusCode}", viewModel.Form.Email, result.StatusCode);
                }
            }
            else
            {
                _logger.LogWarning("SignUp attempt with invalid model state for email: {Email}", viewModel.Form.Email);
            }

            return View(viewModel);
        }

        // Sign out
        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }

        // Sign in
        [HttpGet]
        [Route("/signin")]
        public IActionResult SignIn()
        {
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
        if (result.StatusCode == StatusCodes.Ok)
        {
            // Fetch the user by email or adjust according to your application's logic
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == viewModel.Form.Email);
            
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, viewModel.Form.Email),
                    new Claim("Id", user.Id.ToString()), // Ensure this matches the claim type you are looking for in Details
                    // Add other claims as needed
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                _logger.LogInformation($"User {viewModel.Form.Email} signed in successfully.");
                var isAuthenticated = User.Identity.IsAuthenticated;
                _logger.LogInformation($"User is authenticated: {isAuthenticated}");
                
                _logger.LogInformation("User authenticated successfully: {Email}", viewModel.Form.Email);

                // Redirect to the Details action of AccountController
                _logger.LogInformation("Redirecting to Account/Details");
                return RedirectToAction("Details", "Account");
            }
            else
            {
                _logger.LogWarning("Failed to find user in database after successful sign-in.");
                // Handle the error appropriately, maybe set an error message or log it
            }
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
