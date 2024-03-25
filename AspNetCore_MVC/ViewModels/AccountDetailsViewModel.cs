using AspNetCore_MVC.Models;

namespace AspNetCore_MVC.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";
        public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel()
        {
            ProfileImage = "images/profile-image.svg",
            FirstName = "Lucas",
            LastName = "Wictor",
            Email = "Lucas@domain.com"
        };
        public AccountDetailsAdressInfoModel AddressInfo { get; set; } = new AccountDetailsAdressInfoModel();

    }
}
