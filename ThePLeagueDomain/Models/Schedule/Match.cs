using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class Match
    {
        #region Properties and Fields

        public string Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }        
        [Required]
        public HomeTeam HomeTeam { get; set; }        
        [Required]
        public AwayTeam AwayTeam { get; set; }
        public string SessionID { get; set; }
        [Required]
        public string LeagueID { get; set; }

        #endregion
    }
}
