using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class TeamViewModel
    {
        #region Properties and Fields

        public string Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string LeagueID { get; set; }
        public bool Selected { get; set; }

        #endregion
    }
}
