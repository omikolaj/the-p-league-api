using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Repositories.Merchandise;

namespace ThePLeagueDataCore.Repositories
{
  public class GearSizeRepository : IGearSizeRepository
  {
    #region Fields and Properties
    private readonly ThePLeagueContext _dbContext;

    #endregion

    #region Constructor
    public GearSizeRepository(ThePLeagueContext dbContext)
    {
      this._dbContext = dbContext;
    }
    #endregion

    #region Methods
    private async Task<bool> GearSizeExists(long? id, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(id, ct) != null;
    }
    public async Task<GearSize> AddAsync(GearSize gearSize, CancellationToken ct = default)
    {
      this._dbContext.GearSizes.Add(gearSize);
      await this._dbContext.SaveChangesAsync(ct);
      return gearSize;
    }

    public async Task<bool> DeleteAsync(long? id, CancellationToken ct = default)
    {
      if (!await GearSizeExists(id, ct))
      {
        return false;
      }

      GearSize gearSizeToDelete = this._dbContext.GearSizes.Find(id);
      this._dbContext.GearSizes.Remove(gearSizeToDelete);
      await this._dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<List<GearSize>> GetAllByGearItemIdAsync(long? gearItemId, CancellationToken ct = default)
    {
      return await this._dbContext.GearSizes.Where(gearSize => gearSize.GearItemId == gearItemId).ToListAsync();
    }

    public async Task<GearSize> GetByIDAsync(long? id, CancellationToken ct = default)
    {
      return await _dbContext.GearSizes.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(GearSize gearSize, CancellationToken ct = default)
    {
      if (!await this.GearSizeExists(gearSize.Id, ct))
      {
        return false;
      }
      this._dbContext.GearSizes.Update(gearSize);
      await this._dbContext.SaveChangesAsync(ct);
      return true;
    }

    #endregion
  }
}