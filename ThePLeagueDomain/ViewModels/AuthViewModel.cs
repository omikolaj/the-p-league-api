using System.ComponentModel.DataAnnotations;

namespace ThePLeagueDomain.ViewModels
{
  public class AuthViewModel
  {
    [Required]
    public string UserName { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
  }
}