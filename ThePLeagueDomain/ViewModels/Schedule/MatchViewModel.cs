using System;
using System.Collections.Generic;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class MatchViewModel
    {
        #region Properties and Fields

        public string Id { get; set; }
        public long DateTime { get; set; }        
        public string HomeTeamName { get; set; }
        public string HomeTeamId { get; set; }        
        public string AwayTeamName { get; set; }
        public string AwayTeamId { get; set; }
        public string SessionId { get; set; }
        public string LeagueID { get; set; }        
        public MatchResultViewModel MatchResult { get; set; }

        #endregion
    }
}
