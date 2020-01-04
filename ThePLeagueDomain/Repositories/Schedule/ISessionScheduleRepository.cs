using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.Repositories.Schedule
{
    public interface ISessionScheduleRepository
    {
        #region Method

        Task<LeagueSessionSchedule> AddScheduleAsync(LeagueSessionSchedule newLeagueSessionSchedule, CancellationToken ct = default(CancellationToken));        
        Task<Match> AddMatchAsync(Match newMatch, CancellationToken ct = default(CancellationToken));
        Task<GameDay> AddGameDayAsync(GameDay newGameDay, CancellationToken ct = default);
        Task<GameTime> AddGameTimeAsync(GameTime newGameTime, CancellationToken ct = default);
        Task<List<ActiveSessionInfo>> GetAllActiveSessionsInfoAsync(CancellationToken ct = default);
        Task<List<LeagueSessionSchedule>> GetAllActiveSessionsAsync(CancellationToken ct = default);

        #endregion
    }
}
