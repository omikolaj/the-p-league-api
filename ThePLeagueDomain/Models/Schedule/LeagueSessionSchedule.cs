using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class LeagueSessionSchedule : SessionScheduleBase
    {
        #region Properties and Fields
        public string Id { get; set; }
        public IEnumerable<Match> Matches { get; set; }
        public bool Active { get; set; }

        #endregion

    }
}
