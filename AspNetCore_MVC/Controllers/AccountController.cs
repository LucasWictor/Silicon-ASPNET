using AspNetCore_MVC.Models;
using AspNetCore_MVC.ViewModels;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace AspNetCore_MVC.Controllers
{
    [Authorize] // Ensure user is logged in to access this controller
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(DataContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // Display user details for updates
        // Display user details for updates

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            // Retrieve the user's ID from their claims.
            var userIdClaim = User.FindFirst("Id"); // Adjust "Id" to match the name of the claim containing the user ID.

            // Log the value of userId for debugging purposes.
            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                _logger.LogInformation("User ID: {UserId}", userId);

                // Fetch the user entity from the database.
                var userEntity = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (userEntity == null)
                {
                    // If the user doesn't exist in the database, return NotFound.
                    return NotFound();
                }

                // Populate the view model with data from the user entity.
                var viewModel = new AccountDetailsViewModel
                {
                    BasicInfo = new AccountDetailsBasicInfoModel
                    {
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        Email = userEntity.Email,
                        Phone = userEntity.Phone,
                        Biography = userEntity.Biography
                    }
                    // AddressInfo population omitted for brevity.
                };

                // Return the view with the populated view model.
                return View(viewModel);
            }
            else
            {
                // If no userId found in claims, redirect to login.
                return RedirectToAction("SignIn", "Auth");
            }
        }
        


        // Update user details
        [HttpPost]
        public async Task<IActionResult> UpdateDetails(AccountDetailsViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Signin", "Auth");
            }

            var userEntity = await _context.Users
                                           .Include(u => u.Address)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            if (userEntity == null)
            {
                return NotFound(); // Handle case where user doesn't exist
            }

            // Update user entity with model data
            userEntity.Phone = model.BasicInfo.Phone;
            userEntity.Biography = model.BasicInfo.Biography;

            if (userEntity.Address == null)
            {
                userEntity.Address = new AddressEntity(); // Create new address if it doesn't exist
            }

            userEntity.Address.StreetName = model.AddressInfo.Addressline1;
            userEntity.Address.PostalCode = model.AddressInfo.PostalCode;
            userEntity.Address.City = model.AddressInfo.City;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Account");
        }
    }
}
