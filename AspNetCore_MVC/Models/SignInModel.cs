using System.ComponentModel.DataAnnotations;

namespace AspNetCore_MVC.Models;

public class SignInModel
{
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 0)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage ="Email is required")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password", Order = 1)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }


    [Display(Name = "Remember me",  Order = 2)]
    public bool RememberMe { get; set; }
}
