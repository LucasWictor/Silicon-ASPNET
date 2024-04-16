using AspNetCore_MVC.Models;

namespace AspNetCore_MVC.ViewModels
{
    public class AccountDetailsViewModel
    {
        public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel();
        public AccountDetailsAddressInfoModel AddressInfo { get; set; } = new AccountDetailsAddressInfoModel();
    }
}
