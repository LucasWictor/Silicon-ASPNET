using Infrastructure.Models;

namespace AspNetCore_MVC.ViewModels
{
    public class SignInViewModel
    {
        public string Title { get; set; } = "Sign in";
        public SignInModel Form { get; set; } = new SignInModel();
        public string? ErrorMessage { get; set; }
    }
}
