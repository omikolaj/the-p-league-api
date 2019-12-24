using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class LeagueSessionScheduleViewModel
    {
        #region Fields and Properties

        public string Id { get; set; }
        public IEnumerable<MatchViewModel> Matches { get; set; }
        public bool Active { get; set; }        
        public string LeagueID { get; set; }
        public bool ByeWeeks { get; set; }
        public long? NumberOfWeeks { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public IEnumerable<TeamViewModel> Teams { get; set; }
        public IEnumerable<GameDayViewModel> GamesDays { get; set; }

        #endregion
    }
}
