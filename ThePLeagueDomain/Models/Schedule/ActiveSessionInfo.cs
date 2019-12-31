using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class ActiveSessionInfo
    {
        #region Fields and Properties

        public string Id { get; set; }
        public string LeagueId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion
    }
}
