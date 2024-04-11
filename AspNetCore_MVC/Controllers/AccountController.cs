using AspNetCore_MVC.Models;
using AspNetCore_MVC.ViewModels;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace AspNetCore_MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(DataContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                var userEntity = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == userId);
                if (userEntity == null) { return NotFound(); }

                var viewModel = new AccountDetailsViewModel
                {
                    BasicInfo = new AccountDetailsBasicInfoModel
                    {
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        Email = userEntity.Email,
                        Phone = userEntity.Phone,
                        Biography = userEntity.Biography
                    },
                    AddressInfo = userEntity.Address != null ? new AccountDetailsAddressInfoModel
                    {
                        Addressline1 = userEntity.Address.StreetName,
                        PostalCode = userEntity.Address.PostalCode,
                        City = userEntity.Address.City
                    } : new AccountDetailsAddressInfoModel()
                };
                return View(viewModel);
            }
            else { return RedirectToAction("SignIn", "Auth"); }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetails(AccountDetailsViewModel model)
        {
            if (!ModelState.IsValid) { return View("Details", model); }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) { return RedirectToAction("SignIn", "Auth"); }

            var userEntity = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == userId);
            if (userEntity == null) { return NotFound(); }

            // Updating user basic info
            userEntity.FirstName = model.BasicInfo.FirstName;
            userEntity.LastName = model.BasicInfo.LastName;
            // Consider business logic for email updates
            userEntity.Phone = model.BasicInfo.Phone;
            userEntity.Biography = model.BasicInfo.Biography;

            // Updating address info
            if (userEntity.Address == null) { userEntity.Address = new AddressEntity(); }
            userEntity.Address.StreetName = model.AddressInfo.Addressline1;
            userEntity.Address.PostalCode = model.AddressInfo.PostalCode;
            userEntity.Address.City = model.AddressInfo.City;

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Account");
        }
    }
}
