using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    public async Task<GearImage> AddAsync(GearImage gearSize, CancellationToken ct = default)
    {
      _dbContext.GearImages.Add(gearSize);
      await _dbContext.SaveChangesAsync(ct);
      return gearSize;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<List<GearImage>> GetAllByGearItemIdAsync(long gearItemId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<GearImage> GetByIDAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<bool> UpdateAsync(GearImage gearSize, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}