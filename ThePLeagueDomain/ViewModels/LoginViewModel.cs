using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ThePLeagueDomain.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    public string UserName { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    public ClaimsIdentity Claims { get; set; }

  }
}