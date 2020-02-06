using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class Match
    {
        #region Properties and Fields

        public string MatchId { get; set; }
        public long DateTime { get; set; }                        
        [NotMapped]
        public Team HomeTeam { get; set; }
        public string HomeTeamId { get; set; }
        [NotMapped]
        public Team AwayTeam { get; set; }
        public string AwayTeamId { get; set; }
        public string LeagueSessionScheduleId { get; set; }
        public string LeagueID { get; set; }
        public League League { get; set; }
        public MatchResult MatchResult { get; set; }        

        #endregion
    }
}
