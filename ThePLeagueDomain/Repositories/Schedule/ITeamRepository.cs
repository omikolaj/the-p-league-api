using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThePLeagueDomain.Repositories.Schedule
{
    public interface ITeamRepository
    {
        #region Team
        Task<Models.Schedule.Team> GetByIdAsync(string id, CancellationToken ct = default(CancellationToken));
        Task<Models.Schedule.Team> AddAsync(Models.Schedule.Team newTeam, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Models.Schedule.Team teamToUpdate, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(string id, CancellationToken ct = default(CancellationToken));

        #endregion
    }
}
