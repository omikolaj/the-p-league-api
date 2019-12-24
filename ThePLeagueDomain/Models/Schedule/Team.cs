using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class Team
    {
        #region Properties and Fields

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LeagueID { get; set; }
        public bool Selected { get; set; }
        public IEnumerable<Match> Matches { get; set; }

        #endregion
    }
}
