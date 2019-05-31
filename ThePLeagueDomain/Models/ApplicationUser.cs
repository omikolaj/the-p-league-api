using Microsoft.AspNetCore.Identity;

namespace ThePLeagueDomain.Models
{
  public class ApplicationUser : IdentityUser
  {
    #region Fields and Properties

    public RefreshToken RefreshToken { get; set; }

    #endregion
  }
}