using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Merchandise;

namespace ThePLeagueDomain.Repositories.Merchandise
{
  public interface IGearSizeRepository
  {
    #region Methods
    Task<List<GearSize>> GetAllByGearItemIdAsync(long? id, CancellationToken ct = default(CancellationToken));

    Task<GearSize> GetByIDAsync(long? id, CancellationToken ct = default(CancellationToken));

    Task<GearSize> AddAsync(GearSize gearSize, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateAsync(GearSize gearSize, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteAsync(long? id, CancellationToken ct = default(CancellationToken));

    #endregion
  }
}