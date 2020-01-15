using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private async Task<bool> LeagueSessionScheduleExists(string id, CancellationToken ct = default)
        {
            return await this.GetLeagueSessionScheduleByIdAsync(id, ct) != null;
        }

        public async Task<LeagueSessionSchedule> GetLeagueSessionScheduleByIdAsync(string id, CancellationToken ct = default)
        {
            return await this._dbContext.LeagueSessions.FindAsync(id);
        }

        public async Task<List<ActiveSessionInfo>> GetAllActiveSessionsInfoAsync(CancellationToken ct = default)
        {
            return await this._dbContext.LeagueSessions
                .Where(session => session.Active == true)
                .Select(session => new ActiveSessionInfo
                {
                    Id = session.Id,
                    LeagueId = session.LeagueID,
                    StartDate = session.SessionStart,
                    EndDate = session.SessionEnd
                }).ToListAsync();
        }

        public async Task<List<LeagueSessionSchedule>> GetAllActiveSessionsAsync(CancellationToken ct = default)
        {
            return await this._dbContext.LeagueSessions
                .Where(session => session.Active == true)
                .Include(session => session.Matches)
                    .ThenInclude((Match match) => match.MatchResult)
                .Include(session => session.TeamsSessions)
                    .ThenInclude((TeamSession teamSession) => teamSession.LeagueSessionSchedule)
                .Include(session => session.TeamsSessions)
                    .ThenInclude((TeamSession teamSession) => teamSession.Team)
                .Include(session => session.GamesDays)
                    .ThenInclude((GameDay gameDay) => gameDay.GamesTimes)
                .ToListAsync();
        }

        public async Task<Match> GetMatchByIdAsync(string matchId, CancellationToken ct = default)
        {
            return await this._dbContext.Matches.FindAsync(matchId);
        }

        public async Task<bool> UpdateActiveStatusAsync(LeagueSessionSchedule sessionToUpdate, CancellationToken ct = default)
        {
            if(!await LeagueSessionScheduleExists(sessionToUpdate.Id, ct))
            {
                return false;
            }

            this._dbContext.LeagueSessions.Update(sessionToUpdate);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> UpdateLeagueSessionScheduleAsync(LeagueSessionSchedule leagueSessionScheduleToUpdate, CancellationToken ct = default)
        {
            if (!await LeagueSessionScheduleExists(leagueSessionScheduleToUpdate.Id, ct))
            {
                return false;
            }

            this._dbContext.LeagueSessions.Update(leagueSessionScheduleToUpdate);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task<LeagueSessionSchedule> AddScheduleAsync(LeagueSessionSchedule newLeagueSessionSchedule, CancellationToken ct = default)
        {
            this._dbContext.LeagueSessions.Add(newLeagueSessionSchedule);
            await this._dbContext.SaveChangesAsync(ct);

            return newLeagueSessionSchedule;
        }

        public async Task<GameDay> AddGameDayAsync(GameDay newGameDay, CancellationToken ct = default)
        {
            this._dbContext.GameDays.Add(newGameDay);
            await this._dbContext.SaveChangesAsync(ct);

            return newGameDay;
        }

        public async Task<GameTime> AddGameTimeAsync(GameTime newGameTime, CancellationToken ct = default)
        {
            this._dbContext.Add(newGameTime);
            await this._dbContext.SaveChangesAsync(ct);

            return newGameTime;
        }

        public async Task<Match> AddMatchAsync(Match newMatch, CancellationToken ct = default)
        {
            this._dbContext.Add(newMatch);
            await this._dbContext.SaveChangesAsync(ct);

            return newMatch;
        }

        public async Task<MatchResult> ReportMatchAsync(MatchResult matchResult, CancellationToken ct = default)
        {
            this._dbContext.MatchResults.Update(matchResult);
            await this._dbContext.SaveChangesAsync(ct);

            return matchResult;

        }

        #endregion
    }
}
