using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.Repositories.Schedule;

namespace ThePLeagueDataCore.Repositories.Schedule
{
    public class SportTypeRepository : ISportTypeRepository
    {
        #region Properties and Fields

        private readonly ThePLeagueContext _dbContext;

        #endregion

        #region Constructor

        public SportTypeRepository(ThePLeagueContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion

        #region Methods

        private async Task<bool> SportTypeExists(string id, CancellationToken ct = default)
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public async Task<SportType> AddAsync(SportType newSportType, CancellationToken ct = default)
        {
            this._dbContext.SportTypes.Add(newSportType);
            await this._dbContext.SaveChangesAsync(ct);

            return newSportType;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            if(!await SportTypeExists(id, ct))
            {
                return false;
            }

            SportType sportTypeToDelete = this._dbContext.SportTypes.Find(id);
            this._dbContext.SportTypes.Remove(sportTypeToDelete);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task<SportType> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await this._dbContext.SportTypes.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(SportType sportTypeToUpdate, CancellationToken ct = default)
        {
            if(!await this.SportTypeExists(sportTypeToUpdate.Id))
            {
                return false;
            }

            this._dbContext.SportTypes.Update(sportTypeToUpdate);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<SportType>> GetAllAsync(CancellationToken ct = default)
        {
            return await this._dbContext.SportTypes.Include(sportType => sportType.Leagues).ThenInclude(league => league.Teams).ToListAsync(ct);            
        }

        #endregion
    }
}
