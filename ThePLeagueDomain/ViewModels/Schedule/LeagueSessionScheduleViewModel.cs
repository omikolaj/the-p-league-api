using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class LeagueSessionScheduleViewModel
    {
        #region Fields and Properties

        public string Id { get; set; }
        public ICollection<MatchViewModel> Matches { get; set; }
        public bool Active { get; set; }        
        public string LeagueID { get; set; }
        public string LeagueName { get; set; }
        public string SportTypeID { get; set; }
        public string SportTypeName { get; set; }
        public bool ByeWeeks { get; set; }
        public long? NumberOfWeeks { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public ICollection<TeamSessionViewModel> TeamsSessions { get; set; }
        public ICollection<GameDayViewModel> GamesDays { get; set; }

        #endregion
    }
}
