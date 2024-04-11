using System.ComponentModel.DataAnnotations;

namespace AspNetCore_MVC.Models;

public class AccountDetailsAddressInfoModel
{
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "First name is required")]
    public string Addressline1 { get; set; } = null!;

    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    public string? Addressline2 { get; set; }

    [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 2)]
    [Required(ErrorMessage = "Postal code is required")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = null!;
}

