using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.Repositories.Schedule;

namespace ThePLeagueDataCore.Repositories.Schedule
{
    public class TeamRepository : ITeamRepository
    {
        #region Fields and Properties

        private readonly ThePLeagueContext _dbContext;

        #endregion
        
        #region Constructor

        public TeamRepository(ThePLeagueContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion

        #region Team

        private async Task<bool> TeamExists(string id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public async Task<Team> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await this._dbContext.Teams.FindAsync(id);
        }
        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            if (!await TeamExists(id, ct))
            {
                return false;
            }

            Team teamToDelete = this._dbContext.Teams.Find(id);
            this._dbContext.Teams.Remove(teamToDelete);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task<Team> AddAsync(Team newTeam, CancellationToken ct = default)
        {
            this._dbContext.Teams.Add(newTeam);
            await this._dbContext.SaveChangesAsync(ct);

            return newTeam;
        }

        public async Task<bool> UpdateAsync(Team updatedTeam, CancellationToken ct = default)
        {
            if (!await this.TeamExists(updatedTeam.Id))
            {
                return false;
            }

            this._dbContext.Teams.Update(updatedTeam);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }

        #endregion
    }
}
