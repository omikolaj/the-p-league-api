using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Merchandise;

namespace ThePLeagueDomain.Repositories.Merchandise
{
  public interface IGearImageRepository
  {
    #region Methods
    Task<List<GearImage>> GetAllByGearItemIdAsync(long gearItemId, CancellationToken ct = default(CancellationToken));

    Task<GearImage> GetByIDAsync(long id, CancellationToken ct = default(CancellationToken));

    Task<GearImage> AddAsync(GearImage gearSize, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateAsync(GearImage gearSize, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteAsync(long id, CancellationToken ct = default(CancellationToken));

    #endregion
  }
}