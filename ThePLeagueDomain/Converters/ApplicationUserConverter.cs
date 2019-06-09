using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueDomain.Converters
{
  public static class ApplicationUserConverter
  {
    #region Methods
    public static ApplicationUserViewModel Convert(ApplicationUser user)
    {
      ApplicationUserViewModel userViewModel = new ApplicationUserViewModel();
      userViewModel.Id = user.Id;
      userViewModel.UserName = user.UserName;
      userViewModel.Password = user.PasswordHash;

      return userViewModel;
    }

    #endregion
  }
}