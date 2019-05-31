using System.ComponentModel.DataAnnotations;
using ThePLeagueDomain.Models;

namespace ThePLeagueDomain.ViewModels
{
  public class AuthViewModel
  {
    public ApplicationUser User { get; set; }
    public RefreshToken RefreshToken { get; set; }
    public string Jwt { get; set; }

  }
}