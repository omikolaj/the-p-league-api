using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.Repositories.Schedule
{
    public interface ITeamRepository
    {
        #region Team

        Task<Team> GetByIdAsync(string id, CancellationToken ct = default(CancellationToken));
        Task<Team> AddAsync(Team newTeam, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Team teamToUpdate, CancellationToken ct = default(CancellationToken));        
        Task<bool> DeleteAsync(string id, CancellationToken ct = default(CancellationToken));

        #endregion
    }
}
