using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;

namespace ThePLeagueDomain.Repositories
{
  public interface ILeagueImageRepository
  {
    #region Methods
    Task<LeagueImage> GetByIDAsync(long? id, CancellationToken ct = default(CancellationToken));
    Task<List<LeagueImage>> GetAllAsync(CancellationToken ct = default(CancellationToken));
    Task<bool> DeleteAsync(long? id, CancellationToken ct = default(CancellationToken));
    Task<LeagueImage> AddAsync(LeagueImage leagueImage, CancellationToken ct = default(CancellationToken));
    Task<bool> UpdateAsync(LeagueImage leagueImage, CancellationToken ct = default(CancellationToken));
    Task<IList<LeagueImage>> UpdateAsync(IList<LeagueImage> leagueImages, CancellationToken ct = default(CancellationToken));

    #endregion
  }
}