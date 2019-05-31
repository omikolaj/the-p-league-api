using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueDomain.Supervisor
{
  public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
  {

    #region Methods
    public async Task<bool> DeleteRefreshTokenAsync(ApplicationUserViewModel userViewModel, RefreshToken refreshToken, CancellationToken ct = default)
    {
      ApplicationUser user = await _applicationUserRepository.GetByIDAsync(userViewModel.Id, ct);

      return await _refreshTokenRepository.DeleteAsync(user, refreshToken, ct);
    }

    public async Task<bool> SaveRefreshTokenAsync(ApplicationUserViewModel userViewModel, string refreshToken, CancellationToken ct = default)
    {
      ApplicationUser user = await _applicationUserRepository.GetByIDAsync(userViewModel.Id, ct);

      return await _refreshTokenRepository.SaveAsync(user, refreshToken, ct);
    }

    public async Task<bool> ValidateRefreshTokenAsync(ApplicationUserViewModel userViewModel, RefreshToken refreshToken, CancellationToken ct = default)
    {
      ApplicationUser user = await _applicationUserRepository.GetByIDAsync(userViewModel.Id, ct);

      return await _refreshTokenRepository.ValidateAsync(user, refreshToken, ct);
    }

    #endregion
  }
}