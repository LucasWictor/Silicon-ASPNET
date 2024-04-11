using AspNetCore_MVC.Models;

namespace AspNetCore_MVC.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";
        public AccountDetailsBasicInfoModel BasicInfo { get; set; }
        public AccountDetailsAddressInfoModel AddressInfo { get; set; }
    }

}
