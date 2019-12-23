using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class SessionScheduleBase
    {
        #region Properties and Fields

        public string Id { get; set; }
        public string LeagueID { get; set; }
        public bool ByeWeeks { get; set; }
        public long? NumberOfWeeks { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<GameDay> GamesDays { get; set; }

        #endregion
    }
}
