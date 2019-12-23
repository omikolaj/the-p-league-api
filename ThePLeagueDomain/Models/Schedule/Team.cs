using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class Team
    {
        #region Properties and Fields

        public string Id { get; set; }
        public string Name { get; set; }
        public string LeagueID { get; set; }
        public bool Selected { get; set; }
        public IEnumerable<Match> Matches { get; set; }

        #endregion
    }
}
