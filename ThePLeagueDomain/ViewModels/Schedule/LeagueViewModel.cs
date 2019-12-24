using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class LeagueViewModel
    {
        #region Fields and Properties

        public string Id { get; set; }
        public string Type { get; set; }
        public IEnumerable<TeamViewModel> Teams { get; set; }
        public bool Selected { get; set; }
        public string SportTypeID { get; set; }
        public IEnumerable<LeagueSessionScheduleViewModel> Sessions { get; set; }

        #endregion
    }
}
