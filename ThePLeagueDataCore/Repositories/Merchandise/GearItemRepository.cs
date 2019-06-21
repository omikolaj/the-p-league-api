using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Repositories.Merchandise;

namespace ThePLeagueDataCore.Repositories
{
  public class GearItemRepository : IGearItemRepository
  {
    #region Fields and Properties
    private readonly ThePLeagueContext _dbContext;

    #endregion

    #region Constructor    
    public GearItemRepository(ThePLeagueContext dbContext)
    {
      this._dbContext = dbContext;
    }

    #endregion

    #region Methods
    private async Task<bool> GearItemExists(long? id, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(id, ct) != null;
    }
    public async Task<List<GearItem>> GetAllAsync(CancellationToken ct = default)
    {
      return await this._dbContext.GearItems.Include(gearItem => gearItem.Images).Include(gearItem => gearItem.Sizes).ToListAsync(ct);
    }
    public async Task<GearItem> AddAsync(GearItem gearItem, CancellationToken ct = default)
    {
      this._dbContext.GearItems.Add(gearItem);
      await this._dbContext.SaveChangesAsync(ct);

      return gearItem;
    }

    public async Task<bool> DeleteAsync(long? id, CancellationToken ct = default)
    {
      if (!await GearItemExists(id, ct))
      {
        return false;
      }

      GearItem gearItemToDelete = this._dbContext.GearItems.Find(id);
      _dbContext.GearItems.Remove(gearItemToDelete);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }
    public async Task<GearItem> GetByIDAsync(long? id, CancellationToken ct = default)
    {
      return await this._dbContext.GearItems.Include(gearItem => gearItem.Images).Include(gearItem => gearItem.Sizes).SingleOrDefaultAsync(gearItem => gearItem.Id == id);
    }

    public async Task<bool> UpdateAsync(GearItem gearItem, CancellationToken ct = default)
    {
      if (!await GearItemExists(gearItem.Id, ct))
      {
        return false;
      }
      _dbContext.GearItems.Update(gearItem);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    #endregion
  }
}