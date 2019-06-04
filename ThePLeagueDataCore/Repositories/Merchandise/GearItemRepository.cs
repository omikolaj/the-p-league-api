using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Repositories.Merchandise;

namespace ThePLeagueDataCore.Repositories
{
  public class GearItemRepository : IGearItemRepository
  {
    public Task<GearItem> AddAsync(GearItem gearItem, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public Task<List<GearItem>> GetAllAsync(CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public Task<GearItem> GetByIDAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> UpdateAsync(GearItem geaerItem, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
  }
}