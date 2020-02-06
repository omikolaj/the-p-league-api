using System;
using System.Collections.Generic;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class MatchResultViewModel
    {
        #region Fields and Properties

        public string MatchResultId { get; set; }
        public string MatchId { get; set; }
        //public Match Match { get; set; }
        public string LeagueId { get; set; }        
        public MatchStatus Status { get; set; }
        public long AwayTeamScore { get; set; }
        public string AwayTeamId { get; set; }
        public long HomeTeamScore { get; set; }
        public string HomeTeamId { get; set; }
        public string Score { get; set; }
        public string WonTeamName { get; set; }
        public string LostTeamName { get; set; }
        public string SessionId { get; set; }

        #endregion
    }
}
