using Microsoft.AspNetCore.Identity;

namespace ThePLeagueDomain.Models
{
  public class BaseUser : IdentityUser
  {
    public RefreshToken RefreshToken { get; set; }
  }
}