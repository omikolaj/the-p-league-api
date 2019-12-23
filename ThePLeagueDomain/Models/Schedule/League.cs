using System.Collections.Generic;


namespace ThePLeagueDomain.Models.Schedule
{
    public class League
    {
        #region Fields and Properties

        public long? ID { get; set; }
        public string Type { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public bool Selected { get; set; }
        public long? SportTypeID { get; set; }
        public IEnumerable<LeagueSessionSchedule> Sessions { get; set; }

        #endregion
    }
}
