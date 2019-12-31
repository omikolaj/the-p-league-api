using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class Team
    {
        #region Properties and Fields

        public string Id { get; set; }
        public string Name { get; set; }
        public League League { get; set; }
        public string LeagueID { get; set; }        
        public bool Selected { get; set; }
        public ICollection<Match> Matches { get; set; }
        public virtual ICollection<TeamSession> TeamsSessions { get; set; }

        #endregion
    }
}
