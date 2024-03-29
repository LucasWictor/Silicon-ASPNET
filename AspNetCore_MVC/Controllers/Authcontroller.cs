﻿using AspNetCore_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_MVC.Controllers
{
    public class AuthController : Controller
    {

        //sign up
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

        //sign out
        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }

        //sign in
        [Route("/signin")]
        [HttpGet]
        public IActionResult SignIn()
        {
            var viewModel = new SignInViewModel();
            return View(viewModel);
        }
        [Route("/signin")]
        [HttpPost]
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