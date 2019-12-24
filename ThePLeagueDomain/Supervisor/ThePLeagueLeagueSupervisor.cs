using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.Supervisor
{
    public partial class ThePLeagueLeagueSupervisor : IThePLeagueSupervisor
    {
        public async Task<Team> GetTeamByIdAsync(string id, CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<Team> AddTeamAsync(Team newTeam, CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<bool> UpdateTeamAsync(Team teamToUpdate, CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<List<Team>> AssignTeamsAsync(List<Team> teamsToAssign, CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<List<Team>> UnassignTeamsAsync(List<Team> teamsToUnassign, CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<bool> DeleteTeamAsync(string id, CancellationToken ct = default(CancellationToken))
        {

        }
    }
}
