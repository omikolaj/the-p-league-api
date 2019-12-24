using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDataCore;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.Repositories.Schedule
{
    public class SessionScheduleRepository : ISessionScheduleRepository
    {
        #region Properties and Fields

        private readonly ThePLeagueContext _dbContext;

        #endregion

        #region Constructor

        public SessionScheduleRepository(ThePLeagueContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion

        #region Methods


        public async Task<LeagueSessionSchedule> PublishSessionSchedule(LeagueSessionSchedule newLeagueSessionSchedule, CancellationToken ct = default)
        {
            this._dbContext.LeagueSessions.Add(newLeagueSessionSchedule);
            await this._dbContext.SaveChangesAsync(ct);

            return newLeagueSessionSchedule;
        }

        #endregion
    }
}
