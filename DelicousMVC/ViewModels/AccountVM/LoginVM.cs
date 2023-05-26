using System.ComponentModel.DataAnnotations;

namespace DelicousMVC.ViewModels.AccountVM;

public class LoginVM
{
    [EmailAddress]
    public string EmailAdress { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
