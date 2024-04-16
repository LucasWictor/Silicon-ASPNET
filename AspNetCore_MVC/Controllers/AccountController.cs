using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Infrastructure.Contexts;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AspNetCore_MVC.ViewModels;
using AspNetCore_MVC.Models;

namespace AspNetCore_MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly DataContext _context;

        public AccountController(DataContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Details()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEntity = _context.Users
                .Include(u => u.Address)
                .FirstOrDefault(u => u.Id == userId);

            if (userEntity == null)
            {
                _logger.LogError("User not found in the database.");
                return NotFound(); 
            }

            var viewModel = new AccountDetailsViewModel
            {
                BasicInfo = new AccountDetailsBasicInfoModel
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    Phone = userEntity.Phone,
                    Biography = userEntity.Biography,
                    ProfileImage = userEntity.ProfileImage
                },
                AddressInfo = new AccountDetailsAddressInfoModel
                {
                    Addressline1 = userEntity.Address?.Addressline1,
                    Addressline2 = userEntity.Address?.Addressline2,
                    PostalCode = userEntity.Address?.PostalCode,
                    City = userEntity.Address?.City
                }
            };

            return View(viewModel);
        }

   [HttpPost]
public IActionResult UpdateBasicInfo(AccountDetailsBasicInfoModel basicInfo)
{
    if (!ModelState.IsValid)
    {
        // If the model state is not valid, return the view with validation errors
        return View("Details", new AccountDetailsViewModel { BasicInfo = basicInfo });
    }

    try
    {
        // Get the current user's ID
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        // Retrieve the user entity from the database
        var userEntity = _context.Users.FirstOrDefault(u => u.Id == userId);

        if (userEntity == null)
        {
            // If user entity is not found, return NotFound
            return NotFound(); 
        }

        // Update the basic information
        userEntity.FirstName = basicInfo.FirstName;
        userEntity.LastName = basicInfo.LastName;
        userEntity.Email = basicInfo.Email;
        userEntity.Phone = basicInfo.Phone;
        userEntity.Biography = basicInfo.Biography;

        // Save changes to the database
        _context.SaveChanges();

        // Redirect to the Details action to show updated information
        return RedirectToAction("Details");
    }
    catch (Exception ex)
    {
        // Log any errors that occur during the update process
        _logger.LogError($"Error updating basic information: {ex.Message}");
        
        // You can add a custom error message to ModelState if needed
        ModelState.AddModelError("", "An error occurred while updating basic information.");
        
        // Return the view with the updated model to allow the user to correct any errors
        return View("Details", new AccountDetailsViewModel { BasicInfo = basicInfo });
    }
}

[HttpPost]
public IActionResult UpdateAddressInfo(AccountDetailsAddressInfoModel addressInfo)
{
    if (!ModelState.IsValid)
    {
        // If the model state is not valid, return the view with validation errors
        return View("Details", new AccountDetailsViewModel { AddressInfo = addressInfo });
    }

    try
    {
        // Get the current user's ID
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        // Retrieve the user entity from the database
        var userEntity = _context.Users
            .Include(u => u.Address) // Include the address navigation property
            .FirstOrDefault(u => u.Id == userId);

        if (userEntity == null)
        {
            // If user entity is not found, return NotFound
            return NotFound(); 
        }

        // Update the address information
        userEntity.Address.Addressline1 = addressInfo.Addressline1;
        userEntity.Address.Addressline2 = addressInfo.Addressline2;
        userEntity.Address.PostalCode = addressInfo.PostalCode;
        userEntity.Address.City = addressInfo.City;

        // Save changes to the database
        _context.SaveChanges();

        // Redirect to the Details action to show updated information
        return RedirectToAction("Details");
    }
    catch (Exception ex)
    {
        // Log any errors that occur during the update process
        _logger.LogError($"Error updating address information: {ex.Message}");
        
        // You can add a custom error message to ModelState if needed
        ModelState.AddModelError("", "An error occurred while updating address information.");
        
        // Return the view with the updated model to allow the user to correct any errors
        return View("Details", new AccountDetailsViewModel { AddressInfo = addressInfo });
    }
    } 
    }
}

