using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    public async Task<GearSize> AddAsync(GearSize gearSize, CancellationToken ct = default)
    {
      this._dbContext.GearSizes.Add(gearSize);
      await this._dbContext.SaveChangesAsync(ct);
      return gearSize;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<List<GearSize>> GetAllByGearItemIdAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<GearSize> GetByIDAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<bool> UpdateAsync(GearSize gearSize, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}