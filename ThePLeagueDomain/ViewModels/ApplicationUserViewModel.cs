using ThePLeagueDomain.Models;

namespace ThePLeagueDomain.ViewModels
{
  public class ApplicationUserViewModel
  {
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public RefreshToken RefreshToken { get; set; }
  }
}