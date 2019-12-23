using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class Match
    {
        #region Properties and Fields

        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public string SessionID { get; set; }
        public string LeagueID { get; set; }

        #endregion
    }
}
