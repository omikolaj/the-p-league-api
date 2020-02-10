using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class TeamViewModel
    {
        #region Properties and Fields

        public string Id { get; set; }
        // If user is only updating the name then we want to default Active = true to avoid unintented deletions
        public bool Active { get; set; } = true;
        public string Name { get; set; }
        public string LeagueID { get; set; }
        // If user is only updating the name then we want to default Selected = true to avoid unintented deletions
        public bool Selected { get; set; } = true;

        #endregion
    }
}
