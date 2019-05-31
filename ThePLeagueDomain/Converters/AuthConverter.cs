using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueDomain.Converters
{
  public static class AuthConverter
  {
    #region Methods
    public static AuthViewModel Convert(Auth auth)
    {
      AuthViewModel authViewModel = new AuthViewModel();
      authViewModel.User = auth.User;
      authViewModel.RefreshToken = auth.RefreshToken;

      return authViewModel;
    }

    #endregion
  }
}