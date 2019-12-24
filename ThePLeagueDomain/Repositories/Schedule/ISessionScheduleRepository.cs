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

        Task<LeagueSessionSchedule> PublishSessionSchedule(LeagueSessionSchedule newLeagueSessionSchedule, CancellationToken ct = default(CancellationToken));

        #endregion
    }
}
