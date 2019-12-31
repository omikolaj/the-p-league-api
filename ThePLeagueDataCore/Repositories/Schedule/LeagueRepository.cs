using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.Repositories.Schedule;

namespace ThePLeagueDataCore.Repositories.Schedule
{
    public class LeagueRepository : ILeagueRepository
    {
        #region Property and Fields

        private readonly ThePLeagueContext _dbContext;

        #endregion

        #region Constructor

        public LeagueRepository(ThePLeagueContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion

        private async Task<bool> LeagueExists(string id, CancellationToken ct = default)
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public async Task<League> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await this._dbContext.Leagues.FindAsync(id);
        }
        

        public async Task<List<League>> GetBySportTypeIdAsync(string sportTypeId, CancellationToken ct = default)
        {
            return await this._dbContext.Leagues.Where(league => league.SportTypeID == sportTypeId).ToListAsync();
        }

        public async Task<League> AddAsync(League newLeague, CancellationToken ct = default)
        {
            this._dbContext.Leagues.Add(newLeague);
            await this._dbContext.SaveChangesAsync(ct);

            return newLeague;

        }

        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            if(!await LeagueExists(id, ct))
            {
                return false;
            }

            League leagueToDelete = this._dbContext.Leagues.Find(id);
            this._dbContext.Leagues.Remove(leagueToDelete);
            await this._dbContext.SaveChangesAsync(ct);
            return true;

        }

        public async Task<bool> UpdateAsync(League leagueToUpdate, CancellationToken ct = default)
        {
            if(!await this.LeagueExists(leagueToUpdate.Id, ct))
            {
                return false;
            }
            this._dbContext.Leagues.Update(leagueToUpdate);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }
    }
}
