using AspNetCore_MVC.Models.Components;
using AspNetCore_MVC.Models.Sections;
using AspNetCore_MVC.Models.Views;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_MVC.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            var viewModel = new HomeIndexViewModel
            {
                Title = "Ultimate Task Management Assistant",
                Showcase = new ShowcaseViewModel
                {
                    Id = "Showcase",
                    ShowcaseImage = new() { ImageUrl = "images/taskmaster.svg", AltText = "Showcase image" },
                    Title = "Task Management Assistant You're Going to Love",
                    Text = "We offer you a new generation of task management system. Plan, manage, and track all your tasks in one flexible tool.",
                    link = new() { ControllerName = "Downloads", ActionName = "index", Text = "Get started for free" },
                    BrandsText = "The largest companies use our tool to work efficiently",

                    Brands = 
                    [
                        new() {ImageUrl = "images/brands/brand_1.svg", AltText = "Brand Name 1"},
                        new() {ImageUrl = "images/brands/brand_2.svg", AltText = "Brand Name 2"},
                        new() {ImageUrl = "images/brands/brand_3.svg", AltText = "Brand Name 3"},
                        new() {ImageUrl = "images/brands/brand_4.svg", AltText = "Brand Name 4"}
                    ]
                    

                    
                }
            };

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}