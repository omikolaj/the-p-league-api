using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class SessionScheduleBase
    {
        #region Properties and Fields

        public string LeagueID { get; set; }
        public bool ByeWeeks { get; set; }
        public long? NumberOfWeeks { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public ICollection<TeamSession> TeamsSessions { get; set; } = new Collection<TeamSession>();
        public ICollection<GameDay> GamesDays { get; set; } = new Collection<GameDay>();

        #endregion
    }
}
