using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Supervisor
{
    public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
    {
        #region Methods

        public async Task<LeagueSessionScheduleViewModel> PublishSessionSchedule(LeagueSessionScheduleViewModel newLeagueSessionSchedule, CancellationToken ct = default(CancellationToken))
        {
            // not implemented yet
            return newLeagueSessionSchedule;
        }

        #endregion
    }
}
