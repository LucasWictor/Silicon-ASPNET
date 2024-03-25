using AspNetCore_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_MVC.Controllers;

public class AccountController : Controller
{
    //private readonly AccountService _accountService;

    //public AccountController(AccountService accountService)
    //{
    //    _accountService = accountService;
    //}

    [Route("/account")]
    public IActionResult Details()
    {
        var viewModel = new AccountDetailsViewModel();
        //viewModel.BasicInfo =_accountService.GetBasicInfo();    
        //viewModel.AdressInfo = _accountService.GetAdressInfo();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult BasicInfo(AccountDetailsViewModel viewModel)
    {
        //_accountService.SaveBasicInfo(viewModel.BasicInfo);
        return RedirectToAction(nameof(Details), viewModel);
    }

    [HttpPost]
    public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
    {
        //_accountSerivce:SaveAddressInfo(viewModel.AddressInfo);
        return RedirectToAction(nameof(Details));
    }
}
