using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueDomain
{
  public interface IThePLeagueSupervisor
  {
    #region RefreshToken
    Task<bool> SaveRefreshTokenAsync(ApplicationUserViewModel userViewModel, string refreshToken, CancellationToken ct = default(CancellationToken));
    Task<bool> DeleteRefreshTokenAsync(ApplicationUserViewModel userViewModel, RefreshToken refreshToken, CancellationToken ct = default(CancellationToken));
    Task<bool> ValidateRefreshTokenAsync(ApplicationUserViewModel userViewModel, RefreshToken refreshToken, CancellationToken ct = default(CancellationToken));

    #endregion

    #region ApplicationUser
    Task<List<ApplicationUserViewModel>> GetAllUsersAsync(CancellationToken ct = default(CancellationToken));
    Task<ApplicationUserViewModel> GetUserByIDAsync(string ID, CancellationToken ct = default(CancellationToken));
    Task<ApplicationUserViewModel> AddUserAsync(ApplicationUserViewModel userViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> UpdateUserAsync(ApplicationUserViewModel userViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> DeleteUserAsync(string ID, CancellationToken ct = default(CancellationToken));

    #endregion
  }
}