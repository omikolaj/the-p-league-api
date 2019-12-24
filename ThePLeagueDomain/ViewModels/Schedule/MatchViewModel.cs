using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class MatchViewModel
    {
        #region Properties and Fields

        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public TeamViewModel HomeTeam { get; set; }
        public TeamViewModel AwayTeam { get; set; }
        public string SessionID { get; set; }
        public string LeagueID { get; set; }

        #endregion
    }
}
