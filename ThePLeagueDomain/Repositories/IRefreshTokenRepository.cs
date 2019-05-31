using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;

namespace ThePLeagueDomain.Repositories
{
  public interface IRefreshTokenRepository
  {
    #region Methods
    Task<bool> DeleteAsync(ApplicationUser user, RefreshToken refreshToken, CancellationToken ct = default(CancellationToken));

    Task<bool> SaveAsync(ApplicationUser user, string newRefreshToken, CancellationToken ct = default(CancellationToken));

    Task<bool> ValidateAsync(ApplicationUser user, RefreshToken refreshToken, CancellationToken ct = default(CancellationToken));

    #endregion
  }
}