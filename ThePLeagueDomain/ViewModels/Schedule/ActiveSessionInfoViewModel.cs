using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class ActiveSessionInfoViewModel
    {
        #region Fields and Properties

        public string SessionId { get; set; }
        public string LeagueId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion
    }
}
