using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Repositories.Merchandise;

namespace ThePLeagueDataCore.Repositories
{
  public class GearImageRepository : IGearImageRepository
  {
    #region Fields and Properties
    private readonly ThePLeagueContext _dbContext;

    #endregion

    #region Constructor
    public GearImageRepository(ThePLeagueContext dbContext)
    {
      this._dbContext = dbContext;
    }

    #endregion

    #region Methods

    private async Task<bool> GearImageExists(long? id, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(id, ct) != null;
    }
    public async Task<GearImage> AddAsync(GearImage gearSize, CancellationToken ct = default)
    {
      _dbContext.GearImages.Add(gearSize);
      await _dbContext.SaveChangesAsync(ct);
      return gearSize;
    }

    public async Task<bool> DeleteAsync(long? id, CancellationToken ct = default)
    {
      if (!await GearImageExists(id, ct))
      {
        return false;
      }

      GearImage gearImageToDelete = this._dbContext.GearImages.Find(id);
      _dbContext.GearImages.Remove(gearImageToDelete);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<List<GearImage>> GetAllByGearItemIdAsync(long? gearItemId, CancellationToken ct = default)
    {
      return await this._dbContext.GearImages.Where(gearImage => gearImage.GearItemId == gearItemId).ToListAsync(ct);
    }

    public async Task<GearImage> GetByIDAsync(long? id, CancellationToken ct = default)
    {
      return await this._dbContext.GearImages.FindAsync(id);
    }

    public Task<bool> UpdateAsync(GearImage gearSize, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}