using AspNetCore_MVC.Models.Components;
using AspNetCore_MVC.Models.Sections;

namespace AspNetCore_MVC.Models.Views
{
    public class HomeIndexViewModel
    {
        public string Title { get; set; } = "Ultimate Task Management Assistant";
        public ShowcaseViewModel Showcase { get; set; } = new ShowcaseViewModel
        {
            Id = "overview",
            ShowcaseImage = new ImageViewModel { ImageUrl = "/images/taskmaster.svg", AltText = "Showcase image" },
            Title = "Task Management Assistant You're Going to Love",
            Text = "We offer you a new generation of task management system. Plan, manage, and track all your tasks in one flexible tool.",
            link = new LinkViewModel { ControllerName = "Downloads", ActionName = "index", Text = "Get started for free" },
            BrandsText = "The largest companies use our tool to work efficiently",
            Brands = new List<ImageViewModel>
            {
                new ImageViewModel { ImageUrl = "/images/brands/brand_1.svg", AltText = "Brand Name 1" },
                new ImageViewModel { ImageUrl = "/images/brands/brand_2.svg", AltText = "Brand Name 2" },
                new ImageViewModel { ImageUrl = "/images/brands/brand_3.svg", AltText = "Brand Name 3" },
                new ImageViewModel { ImageUrl = "/images/brands/brand_4.svg", AltText = "Brand Name 4" }
            }
        };
    }
}