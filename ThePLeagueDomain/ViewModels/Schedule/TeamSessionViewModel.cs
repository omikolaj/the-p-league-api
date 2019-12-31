using System;
using System.Collections.Generic;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class TeamSessionViewModel
    {

        #region Fields and Properties

        public string Id { get; set; }
        public string TeamId { get; set; }
        public TeamViewModel Team { get; set; }
        public string LeagueSessionScheduleId { get; set; }
        public LeagueSessionScheduleViewModel LeagueSessionSchedule { get; set; }

        #endregion

    }
}
