﻿using AspNetCore_MVC.Models.Components;

namespace AspNetCore_MVC.Models.Sections
{
    public class ShowcaseViewModel
    {
        public string? Id { get; set; }
        public ImageViewModel ShowcaseImage { get; set; } = null!;
        public string? Title { get; set; }
        public string? Text { get; set; }
        public LinkViewModel link { get; set; } = new LinkViewModel();

        public string? BrandsText { get; set; }
        public  List<ImageViewModel>? Brands { get; set; }

    }
}
