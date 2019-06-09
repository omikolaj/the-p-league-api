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

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<GearItem> GetByIDAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<bool> UpdateAsync(GearItem geaerItem, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}