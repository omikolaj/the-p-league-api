using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.Repositories.Schedule
{
    public interface ILeagueRepository
    {
        #region Methods

        Task<League> GetByIdAsync(string id, CancellationToken ct = default(CancellationToken));
        Task<List<League>> GetBySportTypeIdAsync(string sportTypeId, CancellationToken ct = default(CancellationToken));
        Task<League> AddAsync(League newLeague, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(League leagueToUpdate, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(string id, CancellationToken ct = default(CancellationToken));

        #endregion
    }
}
