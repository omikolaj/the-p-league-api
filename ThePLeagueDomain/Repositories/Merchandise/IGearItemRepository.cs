using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Repositories.Merchandise;

namespace ThePLeagueDomain.Repositories.Merchandise
{
  public interface IGearItemRepository
  {
    #region Methods
    Task<List<GearItem>> GetAllAsync(CancellationToken ct = default(CancellationToken));

    Task<GearItem> GetByIDAsync(long id, CancellationToken ct = default(CancellationToken));

    Task<GearItem> AddAsync(GearItem gearItem, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateAsync(GearItem geaerItem, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteAsync(long id, CancellationToken ct = default(CancellationToken));

    #endregion
  }
}