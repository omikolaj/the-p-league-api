using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public enum MatchStatus
    {
        TBD = 0,
        Completed = 1,
        Canceled = 2,
        Forfeit = 3
    }
    public class MatchResult
    {
        #region Fields and Properties

        public string MatchResultId { get; set; }        
        public string MatchId { get; set; }
        [NotMapped]
        public Match Match { get; set; }
        public string LeagueId { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.TBD;     
        public long AwayTeamScore { get; set; }
        public string AwayTeamId { get; set; }
        public long HomeTeamScore { get; set; }
        public string HomeTeamId { get; set; }
        public string Score { get; set; }
        public string WonTeamName { get; set; }
        public string LostTeamName { get; set; }

        #endregion

    }
}
