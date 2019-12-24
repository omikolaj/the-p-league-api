﻿using System.Collections.Generic;


namespace ThePLeagueDomain.Models.Schedule
{
    public class League
    {
        #region Fields and Properties

        public string Id { get; set; }
        public string Type { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public bool Selected { get; set; }
        public string SportTypeID { get; set; }
        public IEnumerable<LeagueSessionSchedule> Sessions { get; set; }

        #endregion
    }
}
