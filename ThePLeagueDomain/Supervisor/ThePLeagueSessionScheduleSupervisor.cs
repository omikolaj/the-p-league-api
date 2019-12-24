using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.Supervisor
{
    public partial class ThePLeagueSessionScheduleSupervisor : IThePLeagueSupervisor
    {
        public async Task<LeagueSessionSchedule> PublishSessionSchedule(LeagueSessionSchedule newLeagueSessionSchedule, CancellationToken ct = default(CancellationToken))
        {

        }
    }
}
