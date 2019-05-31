using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Repositories;

namespace ThePLeagueDataCore.Repositories
{
  public class ApplicationUserRepository : IApplicationUserRepository
  {
    #region Fields and Properties
    private readonly ThePLeagueContext _dbContext;

    #endregion

    #region Constructor

    public ApplicationUserRepository(ThePLeagueContext dbContext)
    {
      this._dbContext = dbContext;
    }

    #endregion

    #region Methods
    private async Task<bool> UserExists(string id, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(id, ct) != null;
    }
    public void Dspose() => this._dbContext.Dispose();

    public async Task<ApplicationUser> AddAsync(ApplicationUser newUser, CancellationToken ct = default)
    {
      await _dbContext.Users.AddAsync(newUser);
      await _dbContext.SaveChangesAsync(ct);
      return newUser;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<List<ApplicationUser>> GetAllAsync(CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<ApplicationUser> GetByIDAsync(string id, CancellationToken ct = default)
    {
      return await _dbContext.Users.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(ApplicationUser user, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}