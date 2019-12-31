using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class LeagueSessionSchedule : SessionScheduleBase
    {
        #region Properties and Fields
        public string Id { get; set; }
        public ICollection<Match> Matches { get; set; } = new Collection<Match>();
        public bool Active { get; set; }

        #endregion

    }
}
