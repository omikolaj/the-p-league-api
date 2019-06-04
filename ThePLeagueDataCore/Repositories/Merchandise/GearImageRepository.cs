using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Repositories.Merchandise;

namespace ThePLeagueDataCore.Repositories
{
  public class GearImageRepository : IGearImageRepository
  {
    public Task<GearSize> AddAsync(GearSize gearSize, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public Task<List<GearSize>> GetAllByGearItemIdAsync(long gearItemId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public Task<GearSize> GetByIDAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> UpdateAsync(GearSize gearSize, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
  }
}