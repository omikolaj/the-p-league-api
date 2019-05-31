using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Repositories;

namespace ThePLeagueDataCore.Repositories
{
  public class RefreshTokenRepository : IRefreshTokenRepository
  {
    #region Fields and Properties
    private readonly ThePLeagueContext _dbContext;

    #endregion

    #region Constructor
    public RefreshTokenRepository(ThePLeagueContext dbContext)
    {
      this._dbContext = dbContext;
    }

    #endregion

    #region Methods

    public void Dispose() => this._dbContext.Dispose();
    public async Task<bool> DeleteAsync(ApplicationUser user, RefreshToken refreshToken, CancellationToken ct = default)
    {
      RefreshToken userRefreshToken = _dbContext.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken.Token && rt.UserId == user.Id);
      // If the refresh token we have in the database does not match the refresh token
      // passed in from the client, then this refresh token does not belong
      // to this user and we should not delete it. Something went wrong...
      if (user.RefreshToken.Token != user.RefreshToken.Token)
      {
        return false;
      }

      _dbContext.RefreshTokens.Remove(userRefreshToken);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<bool> SaveAsync(ApplicationUser user, string newRefreshToken, CancellationToken ct = default)
    {
      // If the user being passed in already has a refresh token why would we create
      // and try to save another token? The expired refresh token should have been deleted first
      // user.RefreshToken should always be null if were trying to save a new refresh token
      if (user.RefreshToken != null)
      {
        if (!await DeleteAsync(user, user.RefreshToken, ct))
        {
          return false;
        }
      }
      RefreshToken refreshToken = new RefreshToken(newRefreshToken, DateTime.Now.AddDays(5), user.Id);
      await _dbContext.RefreshTokens.AddAsync(refreshToken);
      await _dbContext.SaveChangesAsync(ct);

      return true;
    }

    // Validate if the passed in refresh token matches the one stored in the database 
    public async Task<bool> ValidateAsync(ApplicationUser user, RefreshToken refreshToken, CancellationToken ct = default)
    {
      if (user.RefreshToken.Token != refreshToken.Token)
      {
        return false;
      }
      return _dbContext.RefreshTokens.Any(rt => rt.Token == refreshToken.Token && rt.Active);
    }

    #endregion
  }
}