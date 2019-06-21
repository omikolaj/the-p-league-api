using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.Repositories;

namespace ThePLeagueDataCore.Repositories
{
  public class LeagueImageRepository : ILeagueImageRepository
  {
    #region Fields and Properties
    private readonly ThePLeagueContext _dbContext;

    #endregion

    #region Constructor
    public LeagueImageRepository(ThePLeagueContext dbContext)
    {
      this._dbContext = dbContext;
    }
    #endregion

    #region Methods
    private async Task<bool> LeagueImageExists(long? id, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(id, ct) != null;
    }

    public async Task<LeagueImage> AddAsync(LeagueImage leagueImage, CancellationToken ct = default)
    {
      this._dbContext.LeagueImages.Add(leagueImage);
      await this._dbContext.SaveChangesAsync(ct);

      return leagueImage;
    }

    public async Task<bool> DeleteAsync(long? id, CancellationToken ct = default)
    {
      if (!await LeagueImageExists(id, ct))
      {
        return false;
      }

      LeagueImage leagueImageToDelete = this._dbContext.LeagueImages.Find(id);
      this._dbContext.LeagueImages.Remove(leagueImageToDelete);
      await this._dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<List<LeagueImage>> GetAllAsync(CancellationToken ct = default)
    {
      return await this._dbContext.LeagueImages.ToListAsync(ct);
    }

    public async Task<bool> UpdateAsync(LeagueImage leagueImage, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<LeagueImage> GetByIDAsync(long? id, CancellationToken ct = default)
    {
      return await this._dbContext.LeagueImages.FindAsync(id);
    }

    #endregion
  }
}